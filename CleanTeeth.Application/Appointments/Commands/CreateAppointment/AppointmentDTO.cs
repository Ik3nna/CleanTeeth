namespace CleanTeeth.Application.Appointments.Commands.CreateAppointment;

public class AppointmentDTO
{
    public Guid Id { get; set; }
    public string Status { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public SimpleEntityDTO Patient { get; set; } = null!;
    public SimpleEntityDTO Dentist { get; set; } = null!;
    public SimpleEntityDTO DentalOffice { get; set; } = null!;
}

public class SimpleEntityDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}

