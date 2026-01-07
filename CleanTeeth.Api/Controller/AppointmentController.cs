using CleanTeeth.Api.Contracts.Appointments;
using CleanTeeth.Application.Appointments.Commands.CancelAppointment;
using CleanTeeth.Application.Appointments.Commands.CompleteAppointment;
using CleanTeeth.Application.Appointments.Commands.CreateAppointment;
using CleanTeeth.Application.Appointments.Queries.GetAppointmentDetail;
using CleanTeeth.Application.Appointments.Queries.GetAppointments;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanTeeth.Api.Controller
{
    [Route("api/appointments")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        public IMediator _mediator { get; }
        public AppointmentController (IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Creates a new dental appointment
        /// </summary>
        /// <response code="201">Returns the newly created appointment</response>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<AppointmentDTO>), 201)]
        public async Task<ActionResult<ApiResponse<AppointmentDTO>>> Post([FromBody] CreateAppointmentRequest request)
        {
            var dto = await _mediator.Send(new CreateAppointmentCommand 
                { 
                    PatientId = request.PatientId, 
                    DentistId = request.DentistId, 
                    DentalOfficeId = request.DentalOfficeId, 
                    StartDate = request.StartDate, 
                    EndDate = request.EndDate 
                }
            );
            var response = ApiResponse<AppointmentDTO>.Success(dto, "Appointment created successfully", 201);
            return CreatedAtAction(
                nameof(GetById),
                new { id = dto.Id },
                response
            );
        }

        /// <summary>
        /// Gets an appointment by its ID  
        /// </summary>
        /// <response code="200">Returns the appointment of the specified ID</response>
        [HttpGet("{id:Guid}")]
        [ProducesResponseType(typeof(ApiResponse<AppointmentDetailDTO>), 200)]
        public async Task<ActionResult<ApiResponse<AppointmentDetailDTO>>> GetById([FromRoute] Guid id)
        {
            var dto = await _mediator.Send(new GetAppointmentDetailQuery { Id = id });
            var response = ApiResponse<AppointmentDetailDTO>.Success(dto, $"Appointment with {id} retrieved successfully");
            return Ok(response);
        }

        /// <summary>
        /// Gets a list of appointments
        /// </summary>
        /// <param name="page">Page number (default 1)</param>
        /// <param name="pageSize">Page size (default 10)</param>
        /// <param name="patient">Filter by a patient</param>
        /// <param name="dentist">Filter by an dentist</param>
        /// <param name="dentalOffice">Filter by a dentalOffice</param>
        /// <param name="startDate">Filter by an startDate</param>
        /// <param name="endDate">Filter by an endDate</param>
        /// <response code="200">Returns the list of appointments</response>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<AppointmentListDTO>>), 200)]
        public async Task<ActionResult<ApiResponse<PagedResult<AppointmentListDTO>>>> Get(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] Guid? patient = null, 
            [FromQuery] Guid? dentist = null,
            [FromQuery] Guid? dentalOffice = null, 
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null
        )
        {
            var dto = await _mediator.Send(new GetAppointmentsQuery(page, pageSize, patient, dentist, dentalOffice, startDate, endDate));
            var response = ApiResponse<PagedResult<AppointmentListDTO>>.Success(dto, "Appointments retrieved successfully");
            return Ok(response);
        }

        /// <summary>
        /// Complete an appointment
        /// </summary>
        /// <response code="200">Complete an appointment</response>
        [HttpPost("{id:Guid}/complete")]
        [ProducesResponseType(typeof(ApiResponse<string>), 200)]
        public async Task<ActionResult<ApiResponse<string>>> Complete ([FromRoute] Guid id)
        {
            var command = new CompleteAppointmentCommand{ Id = id };
            await _mediator.Send(command);
            return ApiResponse<string>.Success(null, "Appointment completed successfully");
        }

        /// <summary>
        /// Cancel an appointment
        /// </summary>
        /// <response code="200">Cancel an appointment</response>
        [HttpPost("{id:Guid}/cancel")]
        [ProducesResponseType(typeof(ApiResponse<string>), 200)]
        public async Task<ActionResult<ApiResponse<string>>> Cancel ([FromRoute] Guid id)
        {
            var command = new CancelAppointmentCommand{ Id = id };
            await _mediator.Send(command);
            return ApiResponse<string>.Success(null, "Appointment cancelled successfully");
        }
    }
}
