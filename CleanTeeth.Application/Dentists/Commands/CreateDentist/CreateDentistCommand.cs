using MediatR;

namespace CleanTeeth.Application.Dentists.Commands.CreateDentist;

public class CreateDentistCommand : IRequest<DentistDTO>
{
    public required string Name { get; set; }
    public required string Email { get; set; }
}
