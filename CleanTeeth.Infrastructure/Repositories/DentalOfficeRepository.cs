using CleanTeeth.Domain.Entities;
using CleanTeeth.Domain.Interfaces;
using CleanTeeth.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CleanTeeth.Infrastructure.Repositories;

public class DentalOfficeRepository : IDentalOfficeRepository
{
    private readonly CleanTeethDbContext _dbContext;
    private readonly IUnitOfWork _unitOfWork;

    public DentalOfficeRepository(CleanTeethDbContext dbContext, IUnitOfWork unitOfWork)
    {
        _dbContext = dbContext;
        _unitOfWork = unitOfWork;
    }
    public async Task<DentalOffice> AddDentalOfficeAsync(DentalOffice dentalOffice)
    {
        await _dbContext.DentalOffices.AddAsync(dentalOffice);
        await _unitOfWork.CommitAsync();
        return dentalOffice;
    }

    public async Task DeleteDentalOfficeAsync(Guid id)
    {
        var dentalOffice = await _dbContext.DentalOffices.FindAsync(id);
        if (dentalOffice != null)
        {
            _dbContext.DentalOffices.Remove(dentalOffice);
        }
        await _unitOfWork.CommitAsync();
    }

    public async Task<IEnumerable<DentalOffice>> GetAllDentalOfficesAsync()
    {
        var dentalOffices = await _dbContext.DentalOffices.ToListAsync();
        return dentalOffices;
    }

    public async Task<DentalOffice> GetDentalOfficeByIdAsync(Guid id)
    {
        var dentalOffice = await _dbContext.DentalOffices.FindAsync(id) ?? throw new KeyNotFoundException($"Dental office with ID {id} not found.");
        return dentalOffice;
    }

    public async Task<DentalOffice> UpdateDentalOfficeAsync(DentalOffice dentalOffice)
    {
        _dbContext.DentalOffices.Update(dentalOffice);
        await _unitOfWork.CommitAsync();
        return dentalOffice;
    }
}
