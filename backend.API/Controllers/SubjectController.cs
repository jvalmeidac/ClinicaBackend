using backend.Domain.Commands.Subject.AddSubject;
using backend.Domain.Interfaces.Repositories.Base;
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
    public class SubjectController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUnityOfWork _unitOfWork;

        public SubjectController(IUnityOfWork unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> AddSubject([FromBody] AddSubjectRequest request)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                var response = await _mediator.Send(request, CancellationToken.None);
                _unitOfWork.Commit();
                return Created("", response);
            }
            catch(Exception e)
            {
                _unitOfWork.Rollback();
                return BadRequest(e.Message);
            }
        }
    }
}
