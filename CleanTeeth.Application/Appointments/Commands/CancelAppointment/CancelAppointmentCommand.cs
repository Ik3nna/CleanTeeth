using MediatR;

namespace CleanTeeth.Application.Appointments.Commands.CancelAppointment;

public class CancelAppointmentCommand : IRequest<Unit>
{
    public required Guid Id { get; set; }
}
