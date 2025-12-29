using MediatR;

namespace CleanTeeth.Application.Patients.Commands.CreatePatient;

public class CreatePatientCommand : IRequest<PatientDTO>
{
    public required string Name { get; set; }
    public required string Email { get; set; }
}
