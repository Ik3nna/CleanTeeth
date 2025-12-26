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
        public async Task<IActionResult> Post([FromBody] CreateDentalOfficeRequest request)
        {
            var command = new CreateDentalOfficeCommand
            {
                Name = request.Name
            };

            var dto = await _mediator.Send(command);

            return CreatedAtAction(
                nameof(GetById),
                new { id = dto.Id },
                dto
            );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var dto = await _mediator.Send(new GetDentalOfficeDetailQuery { Id = id });
            return Ok(dto);
        }
    }
}
