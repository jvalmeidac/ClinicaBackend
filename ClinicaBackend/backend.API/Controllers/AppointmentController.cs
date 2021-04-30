using backend.Domain.Commands.Appointment.AddAppointment;
using backend.Domain.Commands.Appointment.ListAppointmentsByPatientId;
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
    public class AppointmentController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUnityOfWork _unityOfWork;

        public AppointmentController(IMediator mediator, IUnityOfWork unityOfWork)
        {
            _mediator = mediator;
            _unityOfWork = unityOfWork;
        }

        [HttpPost]
        public async Task<ActionResult> AddAppointment([FromBody] AddAppointmentRequest request)
        {
            _unityOfWork.BeginTransaction();
            try
            {
                var result = await _mediator.Send(request, CancellationToken.None);
                _unityOfWork.Commit();
                return Created("", result);
            }
            catch (Exception e)
            {
                _unityOfWork.Rollback();
                return BadRequest(e.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult> GetAppointmentsByPatientId(Guid id, 
            [FromQuery] PageParameters pageParameters)
        {
            try
            {
                ListAppointmentsByPatientIdRequest request = new(id, pageParameters);
                var result = await _mediator.Send(request, CancellationToken.None);

                return Ok(result);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
