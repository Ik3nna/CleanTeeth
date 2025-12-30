using AutoMapper;
using CleanTeeth.Domain.Entities;
using CleanTeeth.Domain.Interfaces;
using CleanTeeth.Domain.ValueObjects;
using MediatR;

namespace CleanTeeth.Application.Patients.Commands.CreatePatient;

public class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, PatientDTO>
{
    private readonly IPatientRepository _patientRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreatePatientCommandHandler(
        IPatientRepository patientRepository, 
        IUnitOfWork unitOfWork,
        IMapper mapper
    )
    {
        _patientRepository = patientRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PatientDTO> Handle(CreatePatientCommand command, CancellationToken cancellationToken)
    {
        var patient = new Patient(command.Name, new Email(command.Email));
        try
        {
            await _patientRepository.AddAsync(patient);
            var dto = _mapper.Map<PatientDTO>(patient);
            return dto;
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }
}
