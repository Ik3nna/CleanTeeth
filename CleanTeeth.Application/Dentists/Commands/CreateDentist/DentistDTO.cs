namespace CleanTeeth.Application.Dentists.Commands.CreateDentist;

public class DentistDTO
{
    public Guid Id { get; private set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
}
