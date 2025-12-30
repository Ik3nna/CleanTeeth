using AutoMapper;
using CleanTeeth.Domain.Interfaces;
using MediatR;

namespace CleanTeeth.Application.Patients.Queries.GetPatient;

public class GetPatientQueryHandler : IRequestHandler<GetPatientQuery, PagedResult<GetPatientDTO>>
{
    private readonly IPatientRepository _patientRepository;
    private readonly IMapper _mapper;

    public GetPatientQueryHandler(IPatientRepository patientRepository, IMapper mapper)
    {
        _patientRepository = patientRepository;
        _mapper = mapper;
    }

    public async Task<PagedResult<GetPatientDTO>> Handle(GetPatientQuery request, CancellationToken cancellationToken)
    {
        var patients = await _patientRepository.GetAllAsync(request.page, request.pageSize);
        // Map only the Items
        var dtoItems = _mapper.Map<List<GetPatientDTO>>(patients.Items);
        var dto = new PagedResult<GetPatientDTO>
        {
            Items = dtoItems,
            Page = patients.Page,
            PageSize = patients.PageSize,
            TotalCount = patients.TotalCount
        };
        return dto;
    }
}
