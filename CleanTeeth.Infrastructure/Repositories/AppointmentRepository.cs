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

    public async Task<PagedResult<Appointment>> GetFilteredAsync(
        int page, 
        int pageSize, 
        Guid? patientId, 
        Guid? dentistId, 
        Guid? dentalOfficeId, 
        DateTime? startDate, 
        DateTime? endDate,
        AppointmentStatus? appointmentStatus
    )
    {
        var appointment = _dbContext.Appointments
        .Include(x => x.Patient)
        .Include(x => x.DentalOffice)
        .Include(x => x.Dentist)
        .AsQueryable();

        if (patientId.HasValue) appointment = appointment.Where(x => x.PatientId == patientId.Value);
        if (dentistId.HasValue) appointment = appointment.Where(x => x.DentistId == dentistId.Value);
        if (dentalOfficeId.HasValue) appointment = appointment.Where(x => x.DentalOfficeId == dentalOfficeId);
        if (appointmentStatus.HasValue) appointment = appointment.Where(x => x.Status == appointmentStatus);
        if (startDate.HasValue && endDate.HasValue)
        {
            appointment = appointment.Where(x =>
                startDate.Value < x.TimeInterval.EndTime &&
                endDate.Value > x.TimeInterval.StartTime
            );
        }
        else if (startDate.HasValue)
        {
            appointment = appointment.Where(x => x.TimeInterval.StartTime >= startDate.Value);
        }
        else if (endDate.HasValue)
        {
            appointment = appointment.Where(x => x.TimeInterval.EndTime <= endDate.Value);
        }

        var totalCount = await appointment.CountAsync();

        var items = await appointment
            .OrderBy(x => x.TimeInterval.StartTime)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<Appointment>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyList<Appointment>> GetAppointmentsForReminderAsync(
        DateTime from,
        DateTime to,
        AppointmentStatus status
    )
    {
        return await _dbContext.Appointments
            .Include(x => x.Patient)
            .Include(x => x.Dentist)
            .Include(x => x.DentalOffice)
            .Where(x =>
                x.Status == status &&
                x.TimeInterval.StartTime >= from &&
                x.TimeInterval.EndTime <= to
            )
            .OrderBy(x => x.TimeInterval.StartTime)
            .ToListAsync();
    }
}
