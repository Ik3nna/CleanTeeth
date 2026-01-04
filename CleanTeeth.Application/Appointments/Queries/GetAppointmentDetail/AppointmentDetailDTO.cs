using CleanTeeth.Application.Appointments.Commands.CreateAppointment;

namespace CleanTeeth.Application.Appointments.Queries.GetAppointmentDetail;

public class AppointmentDetailDTO
{
    public Guid Id { get; set; }
    public string Status { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public SimpleEntityDTO Patient { get; set; } = null!;
    public SimpleEntityDTO Dentist { get; set; } = null!;
    public SimpleEntityDTO DentalOffice { get; set; } = null!;
}
