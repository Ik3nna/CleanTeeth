using CleanTeeth.Api.Contracts.DentalOffices;
using CleanTeeth.Application.DentalOffices.Commands.CreateDentalOffice;
using CleanTeeth.Application.DentalOffices.Queries.GetDentalOfficeDetail;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanTeeth.Api.Controller
{
    [Route("api/dentalOffices")]
    [ApiController]
    public class DentalOfficesController : ControllerBase
    {
        public IMediator _mediator { get; }
        public DentalOfficesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<DentalOfficeDTO>), 201)]
        public async Task<ActionResult<DentalOfficeDTO>> Post([FromBody] CreateDentalOfficeRequest request)
        {
            var command = new CreateDentalOfficeCommand
            {
                Name = request.Name
            };

            var dto = await _mediator.Send(command);
            var response = ApiResponse<DentalOfficeDTO>.Success(dto, "Dental office created successfully", 201);

            return CreatedAtAction(
                nameof(GetById),
                new { id = dto.Id },
                response
            );
        }

        [HttpGet("{id:Guid}")]
        [ProducesResponseType(typeof(ApiResponse<DentalOfficeDetailDTO>), 200)]
        public async Task<ActionResult<DentalOfficeDetailDTO>> GetById([FromRoute] Guid id)
        {
            var dto = await _mediator.Send(new GetDentalOfficeDetailQuery { Id = id });
            var response = ApiResponse<DentalOfficeDetailDTO>.Success(dto, $"Dental office with {id} retrieved successfully");
            return Ok(response);
        }
    }
}
