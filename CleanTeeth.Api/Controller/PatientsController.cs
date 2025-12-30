using CleanTeeth.Api.Contracts.Patients;
using CleanTeeth.Application.Patients.Commands.CreatePatient;
using CleanTeeth.Application.Patients.Commands.DeletePatient;
using CleanTeeth.Application.Patients.Commands.UpdatePatient;
using CleanTeeth.Application.Patients.Queries.GetPatient;
using CleanTeeth.Application.Patients.Queries.GetPatientDetail;
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
        public async Task<ActionResult<ApiResponse<PatientDTO>>> Post([FromBody] CreatePatientRequest request)
        {
            var dto = await _mediator.Send(new CreatePatientCommand { Email = request.Email, Name = request.Name });
            var response = ApiResponse<PatientDTO>.Success(dto, "Patient created successfully", 201);
            return CreatedAtAction(
                nameof(GetById),
                new { id = dto.Id },
                response
            );
        }

        /// <summary>
        /// Gets a list of patients
        /// </summary>
        /// <param name="page">Page number (default 1)</param>
        /// <param name="pageSize">Page size (default 10)</param>
        /// <param name="name">Filter by a name</param>
        /// <param name="email">Filter by an email</param>
        /// <response code="200">Returns the list of patients</response>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<GetPatientDTO>>), 200)]
        public async Task<ActionResult<ApiResponse<PagedResult<GetPatientDTO>>>> Get(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? name = null, 
            [FromQuery] string? email = null
        )
        {
            var dto = await _mediator.Send(new GetPatientQuery(page, pageSize, name, email));
            var response = ApiResponse<PagedResult<GetPatientDTO>>.Success(dto, "Patients retrieved successfully");
            return Ok(response);
        }

        /// <summary>
        /// Gets a patient by ID  
        /// </summary>
        /// <response code="200">Returns the patient of the specified ID</response>
        [HttpGet("{id:Guid}")]
        [ProducesResponseType(typeof(ApiResponse<PatientDetailDTO>), 200)]
        public async Task<ActionResult<ApiResponse<PatientDetailDTO>>> GetById([FromRoute] Guid id)
        {
            var dto = await _mediator.Send(new GetPatientDetailQuery { Id = id });
            var response = ApiResponse<PatientDetailDTO>.Success(dto, $"Patient with {id} retrieved successfully");
            return Ok(response);
        }

        /// <summary>
        /// Deletes the patient of the specified ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete ([FromRoute] Guid id)
        {
            await _mediator.Send(new DeletePatientCommand { Id = id });
            var response = ApiResponse<PatientDTO>.Success(null, $"Patient with {id} deleted successfully");
            return Ok(response);
        }

        /// <summary>
        /// Updates the patient of the specified ID
        /// </summary>
        /// <param name="request"></param>
        /// <param name="id"></param>
        ///<response code="200">Returns the updated patient</response>
        [HttpPut("{id:Guid}")]
        [ProducesResponseType(typeof(ApiResponse<PatientDTO>), 200)]
        public async Task<ActionResult<ApiResponse<PatientDTO>>> Put ([FromBody] UpdatePatientRequest request, [FromRoute] Guid id)
        {
            var dto = await _mediator.Send(new UpdatePatientCommand { Id = id, Name = request.Name, Email = request.Email });
            var response = ApiResponse<PatientDTO>.Success(dto, $"Patient updated successfully");
            return Ok(response);
        }
    }
}
