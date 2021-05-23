using backend.API.Security;
using backend.Domain.Commands.Academic.AuthenticateAcademic;
using backend.Domain.Commands.Academic.GetAppointmentsClosed;
using backend.Domain.Commands.Academic.GetAppointmentsOpened;
using backend.Domain.Commands.Academic.ListAllAcademics;
using backend.Domain.Commands.Operator.AddOperator;
using backend.Domain.Interfaces.Repositories.Base;
using backend.Domain.Pagination;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcademicController : ControllerBase
    {
        private readonly IUnityOfWork _unityOfWork;
        private readonly IMediator _mediator;

        public AcademicController(IUnityOfWork unityOfWork, IMediator mediator)
        {
            _unityOfWork = unityOfWork;
            _mediator = mediator;
        }

        [HttpGet]
        [Route("a/" +
            "")]
        public async Task<ActionResult> GetAppointmentsOpened()
        {
            try
            {
                GetAppointmentsOpenedRequest request = new();
                var response = await _mediator.Send(request, CancellationToken.None);

                return Ok(response);
            }
            catch(Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult> GetAppointmentsClosed(Guid id)
        {
            try
            {
                GetAppointmentsClosedRequest request = new(id);
                var response = await _mediator.Send(request, CancellationToken.None);

                return Ok(response);
            }
            catch(Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> GetAcademics([FromQuery] PageParameters pageParameters)
        {
            try
            {
                ListAllAcademicsRequest request = new(pageParameters);
                var response = await _mediator.Send(request, CancellationToken.None);

                return Ok(response);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> AddAcademic([FromBody] AddAcademicRequest request)
        {
            _unityOfWork.BeginTransaction();
            try
            {
                var response = await _mediator.Send(request, CancellationToken.None);
                _unityOfWork.Commit();
                return Created("", response);
            }
            catch (Exception e) 
            {
                _unityOfWork.Rollback();
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("auth")]
        public async Task<ActionResult> AuthenticateCademic(
            [FromBody] AuthenticateAcademicRequest request,
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


        private object GenerateToken(
            AuthenticateAcademicReponse response,
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
                        new Claim("Academic", JsonConvert.SerializeObject(response))
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
