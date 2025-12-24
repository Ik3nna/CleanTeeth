using CleanTeeth.Application.Common.Exceptions;
using CleanTeeth.Domain.Entities;
using CleanTeeth.Domain.Interfaces;
using FluentValidation;
using MediatR;

namespace CleanTeeth.Application.DentalOffices.Commands.CreateDentalOffice;

public class CreateDentalOfficeCommandHandler : IRequestHandler<CreateDentalOfficeCommand, Guid> 
// Guid represents the ID of the created dental office. IRequestHandler interface is from MediatR 
// library and is used to handle requests of type CreateDentalOfficeCommand and return a response of type Guid.
{
    private readonly IDentalOfficeRepository _dentalOfficeRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateDentalOfficeCommand> _validator;

    public CreateDentalOfficeCommandHandler(
        IDentalOfficeRepository dentalOfficeRepository, 
        IUnitOfWork unitOfWork, 
        IValidator<CreateDentalOfficeCommand> validator
    )
    {
        _dentalOfficeRepository = dentalOfficeRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Guid> Handle(CreateDentalOfficeCommand command, CancellationToken cancellationToken) // Guid represents the ID of the created dental office
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new CustomValidationException(validationResult);
        }

        var dentalOffice = new DentalOffice(command.Name);
        try
        {
            await _dentalOfficeRepository.AddDentalOfficeAsync(dentalOffice);
            await _unitOfWork.CommitAsync();
            return dentalOffice.Id;
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
        
    }
}
