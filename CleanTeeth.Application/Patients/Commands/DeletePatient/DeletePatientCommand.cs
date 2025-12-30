using MediatR;

namespace CleanTeeth.Application.Patients.Commands.DeletePatient;

public class DeletePatientCommand : IRequest<Unit>
{
    public required Guid Id { get; set; }
}
