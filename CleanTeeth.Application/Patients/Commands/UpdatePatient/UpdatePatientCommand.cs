using CleanTeeth.Application.Patients.Commands.CreatePatient;
using MediatR;

namespace CleanTeeth.Application.Patients.Commands.UpdatePatient;

public class UpdatePatientCommand : IRequest<PatientDTO>
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
}
