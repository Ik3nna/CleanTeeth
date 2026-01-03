using MediatR;

namespace CleanTeeth.Application.Dentists.Queries.GetDentistDetail;

public class GetDentistDetailQuery : IRequest<DentistDetailDTO>
{
    public required Guid Id { get; set; }
}
