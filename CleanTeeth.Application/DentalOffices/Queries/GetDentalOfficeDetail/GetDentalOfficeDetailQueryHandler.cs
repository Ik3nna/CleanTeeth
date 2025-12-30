using AutoMapper;
using CleanTeeth.Application.Common.Exceptions;
using CleanTeeth.Domain.Interfaces;
using MediatR;

namespace CleanTeeth.Application.DentalOffices.Queries.GetDentalOfficeDetail;

public class GetDentalOfficeDetailQueryHandler : IRequestHandler<GetDentalOfficeDetailQuery, DentalOfficeDetailDTO>
{
    private readonly IDentalOfficeRepository _dentalOfficeRepository;
    private readonly IMapper _mapper;

    public GetDentalOfficeDetailQueryHandler(IDentalOfficeRepository dentalOfficeRepository, IMapper mapper)
    {
        _dentalOfficeRepository = dentalOfficeRepository;
        _mapper = mapper;
    }

    public async Task<DentalOfficeDetailDTO> Handle(GetDentalOfficeDetailQuery request, CancellationToken cancellationToken)
    {
        var dentalOffice = await _dentalOfficeRepository.GetByIdAsync(request.Id);
        if (dentalOffice == null)
        {
            throw new NotFoundException("Dental office not found");
        }

        var dto = _mapper.Map<DentalOfficeDetailDTO>(dentalOffice);
        return dto;
    }
}
