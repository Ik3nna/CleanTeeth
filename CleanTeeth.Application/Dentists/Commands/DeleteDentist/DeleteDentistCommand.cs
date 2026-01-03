using MediatR;

namespace CleanTeeth.Application.Dentists.Commands.DeleteDentist;

public class DeleteDentistCommand : IRequest<Unit>
{
    public required Guid Id { get; set; }
}
