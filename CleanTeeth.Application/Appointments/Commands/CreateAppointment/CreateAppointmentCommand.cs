using MediatR;

namespace CleanTeeth.Application.Appointments.Commands.CreateAppointment;

public class CreateAppointmentCommand : IRequest<AppointmentDTO>
{
    public Guid PatientId { get; set; }
    public Guid DentistId { get; set; }
    public Guid DentalOfficeId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
