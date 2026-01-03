using CleanTeeth.Application.Dentists.Commands.CreateDentist;
using MediatR;

namespace CleanTeeth.Application.Dentists.Commands.UpdateDentist;

public class UpdateDentistCommand : IRequest<DentistDTO>
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
}
