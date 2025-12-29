using AutoMapper;
using CleanTeeth.Domain.Interfaces;
using MediatR;

namespace CleanTeeth.Application.DentalOffices.Queries.GetDentalOfficeQuery;

public class GetDentalOfficeQueryHandler : IRequestHandler<GetDentalOfficeQuery, List<GetDentalOfficesDTO>>
{
    private readonly IDentalOfficeRepository _dentalOfficeRepository;
    private readonly IMapper _mapper;

    public GetDentalOfficeQueryHandler(IDentalOfficeRepository dentalOfficeRepository, IMapper mapper)
    {
        _dentalOfficeRepository = dentalOfficeRepository;
        _mapper = mapper;
    }

    public async Task<List<GetDentalOfficesDTO>> Handle(GetDentalOfficeQuery request, CancellationToken cancellationToken)
    {
        var dentalOffice = await _dentalOfficeRepository.GetAllDentalOfficesAsync();
        var dto = _mapper.Map<List<GetDentalOfficesDTO>>(dentalOffice);
        return dto;
    }
}
