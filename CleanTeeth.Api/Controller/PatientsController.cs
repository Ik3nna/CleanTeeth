using CleanTeeth.Api.Contracts.Patients;
using CleanTeeth.Application.Patients.Commands.CreatePatient;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanTeeth.Api.Controller
{
    [Route("api/patients")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        public IMediator _mediator { get; }
        public PatientsController (IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Creates a patient
        /// </summary>
        /// <response code="201">Returns the newly created patient</response>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<PatientDTO>), 201)]
        public async Task<ActionResult<PatientDTO>> Post([FromBody] CreatePatientRequest request)
        {
            var dto = await _mediator.Send(new CreatePatientCommand { Email = request.Email, Name = request.Name });
            var response = ApiResponse<PatientDTO>.Success(dto, "Patient created successfully", 201);
            return CreatedAtAction(
                nameof(GetById),
                new { id = dto.Id },
                response
            );
        }

        [HttpGet("{id:Guid}")]
        public async Task GetById ()
        {
            
        }
    }
}
