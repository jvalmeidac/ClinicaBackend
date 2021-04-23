using backend.API.Security;
using backend.Domain.Commands.Patient.AddPatient;
using backend.Domain.Commands.Patient.AuthenticatePatient;
using backend.Domain.Commands.Patient.EditPatient;
using backend.Domain.Commands.Patient.ListAllPatients;
using backend.Domain.Commands.Patient.ListOnePatient;
using backend.Domain.Commands.Patient.RemovePatient;
using backend.Domain.Entities;
using backend.Domain.Pagination;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PatientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> AddPatient([FromBody] AddPatientRequest request)
        {
            try
            {
                var result = await _mediator.Send(request, CancellationToken.None);
                return Created("", new { result });
            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> GetPatients([FromQuery] PageParameters pageParameters)
        {
            try
            {
                var request = new ListAllPatientsRequest(pageParameters);
                var result = await _mediator.Send(request, CancellationToken.None);
                return Ok(result);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult> GetPatient(Guid id)
        {
            try
            {
                ListOnePatientRequest request = new(id);
                var result = await _mediator.Send(request, CancellationToken.None);
                return Ok(result);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [AllowAnonymous]
        [HttpPut("{id:Guid}")]
        public async Task<ActionResult> EditPatient(Guid id, [FromBody] EditPatientRequest request)
        {
            try
            {
                request.Id = id;
                var result = await _mediator.Send(request, CancellationToken.None);

                return Ok(result);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> RemovePatient(Guid id)
        {
            try
            {
                RemovePatientRequest request = new(id);
                var response = await _mediator.Send(request, CancellationToken.None);

                return NoContent();

            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("auth")]
        public async Task<ActionResult> AuthenticatePatient(
            [FromBody] AuthenticatePatientRequest request,
            [FromServices] SigningConfigurations signingConfigurations,
            [FromServices] TokenConfigurations tokenConfigurations)
        {
            try
            {
                var authenticatedResponse = await _mediator.Send(request, CancellationToken.None);

                if (authenticatedResponse.Authenticated == true)
                {
                    var response =
                        GenerateToken(authenticatedResponse, signingConfigurations, tokenConfigurations);

                    return Ok(response);
                }

                return Ok(authenticatedResponse);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //[HttpPatch("{id:Guid}")]
        //[Route("password")]
        //public async Task<ActionResult> SwitchPassword(Guid id, [FromBody] Delta<EditPatientRequest> request)
        //{

        //}

        private object GenerateToken(
            AuthenticatePatientResponse response,
            SigningConfigurations signingConfigurations,
            TokenConfigurations tokenConfigurations)
        {
            if (response.Authenticated == true)
            {
                ClaimsIdentity identity = new(
                    new GenericIdentity(response.Id.ToString(), "Id"),
                    new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim("Patient", JsonConvert.SerializeObject(response))
                    }
                );

                DateTime criationDate = DateTime.Now;
                DateTime expirationDate =
                    criationDate + TimeSpan.FromSeconds(tokenConfigurations.Seconds);

                var handler = new JwtSecurityTokenHandler();
                var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                {
                    Issuer = tokenConfigurations.Issuer,
                    Audience = tokenConfigurations.Audience,
                    SigningCredentials = signingConfigurations.SigningCredentials,
                    Subject = identity,
                    NotBefore = criationDate,
                    Expires = expirationDate
                });
                var token = handler.WriteToken(securityToken);

                return new
                {
                    authenticated = true,
                    created = criationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    accessToken = token,
                    firstName = response.FirstName
                };
            }
            else
            {
                return response;
            }
        }
    }
}
