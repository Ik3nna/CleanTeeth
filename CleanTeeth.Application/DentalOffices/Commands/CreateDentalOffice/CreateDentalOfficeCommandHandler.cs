using CleanTeeth.Domain.Entities;
using CleanTeeth.Domain.Interfaces;

namespace CleanTeeth.Application.DentalOffices.Commands.CreateDentalOffice;

public class CreateDentalOfficeCommandHandler
{
    private readonly IDentalOfficeRepository _dentalOfficeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateDentalOfficeCommandHandler(IDentalOfficeRepository dentalOfficeRepository, IUnitOfWork unitOfWork)
    {
        _dentalOfficeRepository = dentalOfficeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateDentalOfficeCommand command) // Guid represents the ID of the created dental office
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
