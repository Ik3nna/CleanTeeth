using CleanTeeth.Application.Common.Exceptions;
using CleanTeeth.Domain.Interfaces;
using MediatR;

namespace CleanTeeth.Application.DentalOffices.Queries.GetDentalOfficeDetail;

public class GetDentalOfficeDetailQueryHandler : IRequestHandler<GetDentalOfficeDetailQuery, DentalOfficeDetailDTO>
{
    private readonly IDentalOfficeRepository _dentalOfficeRepository;

    public GetDentalOfficeDetailQueryHandler(IDentalOfficeRepository dentalOfficeRepository)
    {
        _dentalOfficeRepository = dentalOfficeRepository;
    }

    public async Task<DentalOfficeDetailDTO> Handle(GetDentalOfficeDetailQuery request, CancellationToken cancellationToken)
    {
        var dentalOffice = await _dentalOfficeRepository.GetDentalOfficeByIdAsync(request.Id);
        if (dentalOffice == null)
        {
            throw new NotFoundException("Dental office not found");
        }

        return new DentalOfficeDetailDTO
        {
            Id = dentalOffice.Id,
            Name = dentalOffice.Name
        };
    }
}
