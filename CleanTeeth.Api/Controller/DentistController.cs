using CleanTeeth.Api.Contracts.Dentists;
using CleanTeeth.Application.Dentists.Commands.CreateDentist;
using CleanTeeth.Application.Dentists.Commands.DeleteDentist;
using CleanTeeth.Application.Dentists.Commands.UpdateDentist;
using CleanTeeth.Application.Dentists.Queries.GetDentist;
using CleanTeeth.Application.Dentists.Queries.GetDentistDetail;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanTeeth.Api.Controller
{
    [Route("api/dentists")]
    [ApiController]
    public class DentistsController : ControllerBase
    {
        public IMediator _mediator { get; }
        public DentistsController (IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Creates a dentist
        /// </summary>
        /// <response code="201">Returns the newly created dentist</response>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<DentistDTO>), 201)]
        public async Task<ActionResult<ApiResponse<DentistDTO>>> Post([FromBody] CreateDentistRequest request)
        {
            var dto = await _mediator.Send(new CreateDentistCommand { Email = request.Email, Name = request.Name });
            var response = ApiResponse<DentistDTO>.Success(dto, "Dentist created successfully", 201);
            return CreatedAtAction(
                nameof(GetById),
                new { id = dto.Id },
                response
            );
        }

        /// <summary>
        /// Gets a list of dentists
        /// </summary>
        /// <param name="page">Page number (default 1)</param>
        /// <param name="pageSize">Page size (default 10)</param>
        /// <param name="name">Filter by a name</param>
        /// <param name="email">Filter by an email</param>
        /// <response code="200">Returns the list of dentists</response>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<GetDentistDTO>>), 200)]
        public async Task<ActionResult<ApiResponse<PagedResult<GetDentistDTO>>>> Get(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? name = null, 
            [FromQuery] string? email = null
        )
        {
            var dto = await _mediator.Send(new GetDentistQuery(page, pageSize, name, email));
            var response = ApiResponse<PagedResult<GetDentistDTO>>.Success(dto, "Dentists retrieved successfully");
            return Ok(response);
        }

        /// <summary>
        /// Gets a dentist by ID  
        /// </summary>
        /// <response code="200">Returns the dentist of the specified ID</response>
        [HttpGet("{id:Guid}")]
        [ProducesResponseType(typeof(ApiResponse<DentistDetailDTO>), 200)]
        public async Task<ActionResult<ApiResponse<DentistDetailDTO>>> GetById([FromRoute] Guid id)
        {
            var dto = await _mediator.Send(new GetDentistDetailQuery { Id = id });
            var response = ApiResponse<DentistDetailDTO>.Success(dto, $"Dentist with {id} retrieved successfully");
            return Ok(response);
        }

        /// <summary>
        /// Deletes the dentist of the specified ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete ([FromRoute] Guid id)
        {
            await _mediator.Send(new DeleteDentistCommand { Id = id });
            var response = ApiResponse<DentistDTO>.Success(null, $"Dentist with {id} deleted successfully");
            return Ok(response);
        }

        /// <summary>
        /// Updates the dentist of the specified ID
        /// </summary>
        /// <param name="request"></param>
        /// <param name="id"></param>
        ///<response code="200">Returns the updated dentist</response>
        [HttpPut("{id:Guid}")]
        [ProducesResponseType(typeof(ApiResponse<DentistDTO>), 200)]
        public async Task<ActionResult<ApiResponse<DentistDTO>>> Put ([FromBody] UpdateDentistRequest request, [FromRoute] Guid id)
        {
            var dto = await _mediator.Send(new UpdateDentistCommand { Id = id, Name = request.Name, Email = request.Email });
            var response = ApiResponse<DentistDTO>.Success(dto, $"Dentist updated successfully");
            return Ok(response);
        }
    }
}
