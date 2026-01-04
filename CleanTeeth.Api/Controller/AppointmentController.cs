using CleanTeeth.Api.Contracts.Appointments;
using CleanTeeth.Application.Appointments.Commands.CreateAppointment;
using CleanTeeth.Application.Appointments.Queries.GetAppointmentDetail;
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
    }
}
