using backend.Domain.Commands.Appointment.AddAppointment;
using backend.Domain.Commands.Appointment.ListAppointmentsByPatientId;
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

        public AppointmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> AddAppointment([FromBody] AddAppointmentRequest request)
        {
            try
            {
                var result = await _mediator.Send(request, CancellationToken.None);
                return Created("", result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult> GetAppointmentsByPatientId(Guid id)
        {
            try
            {
                ListAppointmentsByPatientIdRequest request = new(id);
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
