using CleanTeeth.Domain.Interfaces;
using CleanTeeth.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CleanTeeth.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly CleanTeethDbContext _dbContext;
    private readonly IUnitOfWork _unitOfWork;

    public Repository(CleanTeethDbContext dbContext, IUnitOfWork unitOfWork)
    {
        _dbContext = dbContext;
        _unitOfWork = unitOfWork;
    }
    public async Task<T> AddAsync(T entity)
    {
       await _dbContext.Set<T>().AddAsync(entity);
       await _unitOfWork.CommitAsync();
       return entity;
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);
        _dbContext.Set<T>().Remove(entity);
        await _unitOfWork.CommitAsync();
    }

    public async Task<PagedResult<T>> GetAllAsync(int page = 1, int pageSize = 10)
    {
        var query = _dbContext.Set<T>().AsNoTracking();

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<T>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        return await _dbContext.Set<T>().FindAsync(id) ?? throw new KeyNotFoundException($"{typeof(T).Name} with ID: {id} not found");
    }

    public async Task<T> UpdateAsync(T entity)
    {
        _dbContext.Set<T>().Update(entity);
        await _unitOfWork.CommitAsync();
        return entity;
    }
}
