namespace CleanTeeth.Application.Notifications;

public interface INotifications
{
    Task SendAppointmentConfirmation(AppointmentConfirmationDTO appointmentConfirmationDTO);
}
