using CleanTeeth.Domain.Entities;
using CleanTeeth.Domain.Interfaces;
using MediatR;

namespace CleanTeeth.Application.DentalOffices.Commands.CreateDentalOffice;

public class CreateDentalOfficeCommandHandler : IRequestHandler<CreateDentalOfficeCommand, Guid> 
// Guid represents the ID of the created dental office. IRequestHandler interface is from MediatR 
// library and is used to handle requests of type CreateDentalOfficeCommand and return a response of type Guid.
{
    private readonly IDentalOfficeRepository _dentalOfficeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateDentalOfficeCommandHandler(
        IDentalOfficeRepository dentalOfficeRepository, 
        IUnitOfWork unitOfWork
    )
    {
        _dentalOfficeRepository = dentalOfficeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateDentalOfficeCommand command, CancellationToken cancellationToken) // Guid represents the ID of the created dental office
    {
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
