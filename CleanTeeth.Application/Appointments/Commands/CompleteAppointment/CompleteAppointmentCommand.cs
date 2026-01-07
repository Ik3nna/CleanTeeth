using MediatR;

namespace CleanTeeth.Application.Appointments.Commands.CompleteAppointment;

public class CompleteAppointmentCommand : IRequest<Unit>
{
    public required Guid Id { get; set; }
}
