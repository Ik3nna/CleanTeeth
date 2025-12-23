using CleanTeeth.Domain.Enums;
using CleanTeeth.Domain.Exceptions;

namespace CleanTeeth.Domain.Entities;

public class Appointment
{
    public Guid Id { get; private set; }
    public Guid PatientId { get; private set; }
    public Guid DentistId { get; private set; }
    public Guid DentalOfficeId { get; private set; }
    public AppointmentStatus Status { get; private set; }
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }
    // public TimeInterval TimeInterval { get; private set; }
    public Patient? Patient { get; private set; }
    public Dentist? Dentist { get; private set; }
    public DentalOffice? DentalOffice { get; private set; }

    public Appointment(Guid patientId, Guid dentistId, Guid dentalOfficeId, DateTime startTime, DateTime endTime)
    {
        if (startTime > endTime)
        {
            throw new BusinessRuleException("The start time cannot be after the end time of the appointment.");
        }

        if (startTime < DateTime.UtcNow)
        {
            throw new BusinessRuleException("The start time cannot be in the past.");
        }

        PatientId = patientId;
        DentistId = dentistId;
        DentalOfficeId = dentalOfficeId;
        Status = AppointmentStatus.Scheduled;
        StartTime = startTime;
        EndTime = endTime;
        Id = Guid.CreateVersion7();
    }

    public void Cancel ()
    {
        if (Status != AppointmentStatus.Scheduled)
        {
            throw new BusinessRuleException("Only scheduled appointments can be cancelled.");
        }

        Status = AppointmentStatus.Cancelled;
    }

    public void Complete ()
    {
        if (Status != AppointmentStatus.Scheduled)
        {
            throw new BusinessRuleException("Only scheduled appointments can be completed.");
        }

        Status = AppointmentStatus.Completed;
    }
}
