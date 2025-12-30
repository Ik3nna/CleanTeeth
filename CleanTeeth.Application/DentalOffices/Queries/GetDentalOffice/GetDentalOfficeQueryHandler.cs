using AutoMapper;
using CleanTeeth.Domain.Interfaces;
using MediatR;

namespace CleanTeeth.Application.DentalOffices.Queries.GetDentalOfficeQuery;

public class GetDentalOfficeQueryHandler : IRequestHandler<GetDentalOfficeQuery, PagedResult<GetDentalOfficesDTO>>
{
    private readonly IDentalOfficeRepository _dentalOfficeRepository;
    private readonly IMapper _mapper;

    public GetDentalOfficeQueryHandler(IDentalOfficeRepository dentalOfficeRepository, IMapper mapper)
    {
        _dentalOfficeRepository = dentalOfficeRepository;
        _mapper = mapper;
    }

    public async Task<PagedResult<GetDentalOfficesDTO>> Handle(GetDentalOfficeQuery request, CancellationToken cancellationToken)
    {
        var dentalOffice = await _dentalOfficeRepository.GetAllAsync(request.page, request.pageSize);
        // Map the items
        var dtoItems = _mapper.Map<List<GetDentalOfficesDTO>>(dentalOffice.Items);
        var dto = new PagedResult<GetDentalOfficesDTO>
        {
            Items = dtoItems,
            Page = dentalOffice.Page,
            PageSize = dentalOffice.PageSize,
            TotalCount = dentalOffice.TotalCount
        };
        return dto;
    }
}
