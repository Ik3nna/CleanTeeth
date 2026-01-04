using CleanTeeth.Domain.Entities;
using CleanTeeth.Domain.Interfaces;
using CleanTeeth.Infrastructure.Data;
using CleanTeeth.Domain.Enums;
using Microsoft.EntityFrameworkCore;
namespace CleanTeeth.Infrastructure.Repositories;

public class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
{
    private readonly CleanTeethDbContext _dbContext;   
    public AppointmentRepository(
        CleanTeethDbContext dbContext, 
        IUnitOfWork unitOfWork
    ) : base(dbContext, unitOfWork)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> OverlapExists(Guid dentistId, DateTime startDate, DateTime endDate)
    {
        return await _dbContext.Appointments
            .Where(x => x.DentistId == dentistId && x.Status == AppointmentStatus.Scheduled 
                && startDate < x.TimeInterval.EndTime && endDate > x.TimeInterval.StartTime
            )
            .AnyAsync();
    }

    // The new overwrites the GetByIdAsync already defined in Repository
    new public async Task<Appointment?> GetByIdAsync (Guid id)
    {
        var appointment = await _dbContext.Appointments
        .Include(x => x.Patient)
        .Include(x => x.Dentist)
        .Include(x => x.DentalOffice)
        .FirstOrDefaultAsync(x => x.Id == id); 

        if (appointment == null)
            throw new KeyNotFoundException($"Appointment with id {id} not found");

        return appointment;
    }
}
