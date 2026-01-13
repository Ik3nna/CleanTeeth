using CleanTeeth.Application.Appointments.Commands.CreateAppointment;
using CleanTeeth.Domain.Enums;
using MediatR;

namespace CleanTeeth.Application.Appointments.Commands.SendAppointmentReminder;

public class SendAppointmentReminderCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public AppointmentStatus Status { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public SimpleEntityDTO Patient { get; set; } = null!;
    public SimpleEntityDTO Dentist { get; set; } = null!;
    public SimpleEntityDTO DentalOffice { get; set; } = null!;
}
