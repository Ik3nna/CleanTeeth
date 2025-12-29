using CleanTeeth.Application.DentalOffices.Commands.CreateDentalOffice;
using MediatR;

namespace CleanTeeth.Application.DentalOffices.Commands.UpdateDentalOffice;

public class UpdateDentalOfficeCommand : IRequest<DentalOfficeDTO>
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
}
