using AutoMapper;
using CleanTeeth.Domain.Interfaces;
using MediatR;

namespace CleanTeeth.Application.Dentists.Queries.GetDentist;

public class GetDentistQueryHandler : IRequestHandler<GetDentistQuery, PagedResult<GetDentistDTO>>
{
    private readonly IDentistRepository _dentistRepository;
    private readonly IMapper _mapper;

    public GetDentistQueryHandler(IDentistRepository dentistRepository, IMapper mapper)
    {
        _dentistRepository = dentistRepository;
        _mapper = mapper;
    }

    public async Task<PagedResult<GetDentistDTO>> Handle(GetDentistQuery request, CancellationToken cancellationToken)
    {
        var dentist = await _dentistRepository.GetFilteredAsync(request.page, request.pageSize, request.name, request.email);
        // Map only the Items
        var dtoItems = _mapper.Map<List<GetDentistDTO>>(dentist.Items);
        var dto = new PagedResult<GetDentistDTO>
        {
            Items = dtoItems,
            Page = dentist.Page,
            PageSize = dentist.PageSize,
            TotalCount = dentist.TotalCount
        };
        return dto;
    }
}
