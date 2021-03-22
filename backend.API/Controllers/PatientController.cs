using backend.Domain.Commands.Patient.AddPatient;
using backend.Domain.Commands.Patient.EditPatient;
using backend.Domain.Commands.Patient.ListAllPatients;
using backend.Domain.Commands.Patient.ListOnePatient;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
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

        [HttpPost]
        public async Task<ActionResult> AddPatient([FromBody] AddPatientRequest request)
        {
            try
            {
                var result = await _mediator.Send(request, CancellationToken.None);
                return Created("",new { result });
            }
            catch(Exception e)
            {
                return Conflict(e.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetPatients()
        {
            try
            {
                var request = new ListAllPatientsRequest();
                var result = await _mediator.Send(request, CancellationToken.None);
                return Ok(result);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult> GetPatient(Guid id)
        {
            try
            {
                var request = new ListOnePatientRequest(id);
                var result = await _mediator.Send(request, CancellationToken.None);
                return Ok(result);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult> EditPatient(Guid id, 
            [FromBody] EditPatientRequest request)
        {
            try
            {
                request.Id = id;
                var result = await _mediator.Send(request, CancellationToken.None);

                return Ok(request);
            }
            catch(Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
