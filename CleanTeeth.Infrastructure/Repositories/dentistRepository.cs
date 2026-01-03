using CleanTeeth.Domain.Entities;
using CleanTeeth.Domain.Interfaces;
using CleanTeeth.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using CleanTeeth.Domain.ValueObjects;

namespace CleanTeeth.Infrastructure.Repositories;

public class DentistRepository : Repository<Dentist>, IDentistRepository
{
    private readonly CleanTeethDbContext _dbContext;

    public DentistRepository(
        CleanTeethDbContext dbContext, 
        IUnitOfWork unitOfWork
    ) : base(dbContext, unitOfWork)
    {
        _dbContext = dbContext;
    }

    public async Task<PagedResult<Dentist>> GetFilteredAsync(int page, int pageSize, string? name, string? email)
    {
        var query = _dbContext.Dentists.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(name))
        {
            query = query.Where(p =>
                p.Name.Contains(name));
        }

        if (!string.IsNullOrWhiteSpace(email))
        {
            query = query.Where(p => p.Email == new Email(email));
        }

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<Dentist>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };
        throw new NotImplementedException();
    }
}
