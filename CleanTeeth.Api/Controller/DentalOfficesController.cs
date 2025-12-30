using CleanTeeth.Api.Contracts.DentalOffices;
using CleanTeeth.Application.DentalOffices.Commands.CreateDentalOffice;
using CleanTeeth.Application.DentalOffices.Commands.DeleteDentalOffice;
using CleanTeeth.Application.DentalOffices.Commands.UpdateDentalOffice;
using CleanTeeth.Application.DentalOffices.Queries.GetDentalOfficeDetail;
using CleanTeeth.Application.DentalOffices.Queries.GetDentalOfficeQuery;
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

        /// <summary>
        /// Creates a dental office
        /// </summary>
        /// <response code="201">Returns the newly created dental office</response>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<DentalOfficeDTO>), 201)]
        public async Task<IActionResult> Post([FromBody] CreateDentalOfficeRequest request)
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

        /// <summary>
        /// Gets a dental office by ID  
        /// </summary>
        /// <response code="200">Returns the dental office of the specified ID</response>
        [HttpGet("{id:Guid}")]
        [ProducesResponseType(typeof(ApiResponse<DentalOfficeDetailDTO>), 200)]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var dto = await _mediator.Send(new GetDentalOfficeDetailQuery { Id = id });
            var response = ApiResponse<DentalOfficeDetailDTO>.Success(dto, $"Dental office with {id} retrieved successfully");
            return Ok(response);
        }

        /// <summary>
        /// Gets a list of dental offices
        /// </summary>
        /// <param name="page">Page number (default 1)</param>
        /// <param name="pageSize">Page size (default 10)</param>
        /// <response code="200">Returns the list of dental offices</response>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<GetDentalOfficesDTO>>), 200)]
        public async Task<ActionResult<ApiResponse<PagedResult<GetDentalOfficesDTO>>>> Get(
            [FromQuery] int page = 1, 
            [FromQuery] int pageSize = 10)
        {
            // Pass the query parameters to the MediatR query
            var query = new GetDentalOfficeQuery(page, pageSize);
            var dto = await _mediator.Send(query);

            var response = ApiResponse<PagedResult<GetDentalOfficesDTO>>.Success(dto, "Dental offices retrieved successfully");
            return Ok(response);
        }

        /// <summary>
        /// Updates the dental office of the specified ID
        /// </summary>
        /// <param name="request"></param>
        /// <param name="id"></param>
        ///<response code="200">Returns the updated dental office</response>
        [HttpPut("{id:Guid}")]
        [ProducesResponseType(typeof(ApiResponse<DentalOfficeDTO>), 200)]
        public async Task<ActionResult<ApiResponse<DentalOfficeDTO>>> Put ([FromBody] UpdateDentalOfficeRequest request, [FromRoute] Guid id)
        {
            var dto = await _mediator.Send(new UpdateDentalOfficeCommand { Id = id, Name = request.Name });
            var response = ApiResponse<DentalOfficeDTO>.Success(dto, $"Dental office updated successfully");
            return Ok(response);
        }

        /// <summary>
        /// Deletes the dental office of the specified ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete ([FromRoute] Guid id)
        {
            await _mediator.Send(new DeleteDentalOfficeCommand { Id = id });
            var response = ApiResponse<DentalOfficeDTO>.Success(null, $"Dental office with {id} deleted successfully");
            return Ok(response);
        }
    }
}
