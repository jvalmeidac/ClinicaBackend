using backend.API.Security;
using backend.Domain.Commands.Admin.DelegateAcademic;
using backend.Domain.Interfaces.Repositories.Base;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading;
using System.Threading.Tasks;

namespace backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUnityOfWork _unityOfWork;

        public AdminController(IMediator mediator, IUnityOfWork unityOfWork)
        {
            _mediator = mediator;
            _unityOfWork = unityOfWork;
        }

        [HttpPost]
        public async Task<ActionResult> DelegateAcademic([FromBody]DelegateAcademicRequest request)
        {
            _unityOfWork.BeginTransaction();
            try
            {
                var result = await _mediator.Send(request, CancellationToken.None);
                _unityOfWork.Commit();
                return Created("", result);
            }
            catch(Exception e)
            {
                _unityOfWork.Rollback();
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("auth")]
        [AllowAnonymous]
        public IActionResult LoginAdmin(dynamic login,
            [FromServices] SigningConfigurations signingConfigurations,
            [FromServices] TokenConfigurations tokenConfigurations)
        {
            try
            {
                if (login.user == "admin" && login.password == "admin")
                {
                    var response = GenerateToken(login, signingConfigurations, tokenConfigurations);
                    return Ok(response);
                }
                return Ok(new { Authenticated = false });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        private object GenerateToken(
            dynamic login,
            SigningConfigurations signingConfigurations,
            TokenConfigurations tokenConfigurations)
        {
            if (login.user == "admin" && login.password == "admin")
            {

                DateTime criationDate = DateTime.Now;
                DateTime expirationDate =
                    criationDate + TimeSpan.FromSeconds(tokenConfigurations.Seconds);

                var handler = new JwtSecurityTokenHandler();
                var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                {
                    Issuer = tokenConfigurations.Issuer,
                    Audience = tokenConfigurations.Audience,
                    SigningCredentials = signingConfigurations.SigningCredentials,
                    NotBefore = criationDate,
                    Expires = expirationDate
                });
                var token = handler.WriteToken(securityToken);

                return new
                {
                    authenticated = true,
                    created = criationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    accessToken = token
                };
            }
            else
            {
                return new { Authenticated = false };
            }
        }
    }
}
