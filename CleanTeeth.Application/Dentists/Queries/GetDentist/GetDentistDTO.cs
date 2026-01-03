namespace CleanTeeth.Application.Dentists.Queries.GetDentist;

public class GetDentistDTO
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
}
