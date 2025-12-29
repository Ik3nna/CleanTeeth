namespace CleanTeeth.Application.Patients.Queries.GetPatient;

public class GetPatientDTO
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
}
