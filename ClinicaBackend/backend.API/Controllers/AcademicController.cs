using backend.Domain.Commands.Academic.ListAllAcademics;
using backend.Domain.Commands.Operator.AddOperator;
using backend.Domain.Interfaces.Repositories.Base;
using backend.Domain.Pagination;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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
    }
}
