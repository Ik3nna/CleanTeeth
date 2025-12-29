namespace CleanTeeth.Application.Patients.Commands.CreatePatient;

public class PatientDTO
{
    public Guid Id { get; private set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
}
