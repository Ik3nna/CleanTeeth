using System;
using AutoMapper;
using CleanTeeth.Application.Common.Exceptions;
using CleanTeeth.Domain.Interfaces;
using MediatR;

namespace CleanTeeth.Application.Patients.Commands.DeletePatient;

public class DeletePatientCommandHandler : IRequestHandler<DeletePatientCommand, Unit>
{
    private readonly IPatientRepository _patientRepository;
    private readonly IUnitOfWork _unitOfWork;
    public DeletePatientCommandHandler(
        IPatientRepository patientRepository, 
        IUnitOfWork unitOfWork,
        IMapper mapper
    )
    {
        _patientRepository = patientRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeletePatientCommand command, CancellationToken cancellationToken)
    {
        var patient = await _patientRepository.GetByIdAsync(command.Id);

        if (patient == null)
        {
            throw new NotFoundException($"The patient with id: {command.Id} was not found");
        }
        
        try
        {
            await _patientRepository.DeleteAsync(patient.Id);
            return Unit.Value;
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }
}
