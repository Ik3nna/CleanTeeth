namespace CleanTeeth.Application.Notifications;

public class AppointmentConfirmationDTO
{
    public required Guid Id { get; set; }
    public required string Patient { get; set; }
    public required string Patient_Email { get; set; }
    public required string Dentist { get; set; }
    public required string DentalOffice { get; set; }
    public required DateTime Date { get; set; }
}
