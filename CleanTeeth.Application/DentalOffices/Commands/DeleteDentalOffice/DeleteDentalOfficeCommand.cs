using MediatR;

namespace CleanTeeth.Application.DentalOffices.Commands.DeleteDentalOffice;

public class DeleteDentalOfficeCommand : IRequest<Unit>
{
    public required Guid Id { get; set; }
}
