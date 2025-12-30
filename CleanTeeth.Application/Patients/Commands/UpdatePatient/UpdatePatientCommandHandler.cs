using AutoMapper;
using CleanTeeth.Application.Common.Exceptions;
using CleanTeeth.Application.Patients.Commands.CreatePatient;
using CleanTeeth.Domain.Interfaces;
using MediatR;

namespace CleanTeeth.Application.Patients.Commands.UpdatePatient;

public class UpdatePatientCommandHandler : IRequestHandler<UpdatePatientCommand, PatientDTO>
{
     private readonly IPatientRepository _patientRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdatePatientCommandHandler(
        IPatientRepository patientRepository, 
        IUnitOfWork unitOfWork,
        IMapper mapper
    )
    {
        _patientRepository = patientRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PatientDTO> Handle(UpdatePatientCommand command, CancellationToken cancellationToken)
    {
        var patient = await _patientRepository.GetByIdAsync(command.Id);

        if (patient == null)
        {
            throw new NotFoundException($"The dental office with id: {command.Id} was not founf");
        }
        
        patient.UpdatePatient(command.Name, command.Email);

        try
        {
            await _patientRepository.UpdateAsync(patient);
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
