using MediatR;

namespace CleanTeeth.Application.DentalOffices.Queries.GetDentalOfficeDetail;

public class GetDentalOfficeDetailQuery : IRequest<DentalOfficeDetailDTO>
// IRequest requires a type parameter specifying the response type
// In this case, the response type is DentalOfficeDTO
// It is required by MediatR to define the expected response for the query
{
    public required Guid Id { get; set; }
}
