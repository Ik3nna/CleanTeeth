using CleanTeeth.Domain.Entities;

namespace CleanTeeth.Domain.Interfaces;

public interface IAppointmentRepository : IRepository<Appointment>
{
    Task<bool> OverlapExists (Guid dentistId, DateTime startDate, DateTime endDate); 

    // The new overwrites the GetByIdAsync already defined in IRepository
    new Task<Appointment> GetByIdAsync (Guid id);
}
