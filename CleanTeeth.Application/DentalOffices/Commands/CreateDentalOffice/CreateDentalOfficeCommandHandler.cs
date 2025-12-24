using CleanTeeth.Domain.Entities;
using CleanTeeth.Domain.Interfaces;

namespace CleanTeeth.Application.DentalOffices.Commands.CreateDentalOffice;

public class CreateDentalOfficeCommandHandler
{
    private readonly IDentalOfficeRepository _dentalOfficeRepository;

    public CreateDentalOfficeCommandHandler(IDentalOfficeRepository dentalOfficeRepository)
    {
        _dentalOfficeRepository = dentalOfficeRepository;
    }

    public async Task<Guid> Handle(CreateDentalOfficeCommand command) // Guid represents the ID of the created dental office
    {
        var dentalOffice = new DentalOffice(command.Name);
        await _dentalOfficeRepository.AddDentalOfficeAsync(dentalOffice);
        return dentalOffice.Id;
    }
}
