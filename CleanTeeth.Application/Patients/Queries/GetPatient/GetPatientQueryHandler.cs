using System;
using AutoMapper;
using CleanTeeth.Domain.Interfaces;
using MediatR;

namespace CleanTeeth.Application.Patients.Queries.GetPatient;

public class GetPatientQueryHandler : IRequestHandler<GetPatientQuery, List<GetPatientDTO>>
{
    private readonly IPatientRepository _patientRepository;
    private readonly IMapper _mapper;

    public GetPatientQueryHandler(IPatientRepository patientRepository, IMapper mapper)
    {
        _patientRepository = patientRepository;
        _mapper = mapper;
    }

    public async Task<List<GetPatientDTO>> Handle(GetPatientQuery request, CancellationToken cancellationToken)
    {
        var patient = await _patientRepository.GetAllPatientsAsync();
        var dto = _mapper.Map<List<GetPatientDTO>>(patient);
        return dto;
    }
}
