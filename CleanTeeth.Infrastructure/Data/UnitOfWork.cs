using CleanTeeth.Domain.Interfaces;

namespace CleanTeeth.Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly CleanTeethDbContext _dbContext;

    public UnitOfWork(CleanTeethDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task CommitAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public Task RollbackAsync()
    {
        return Task.CompletedTask;
    }
}
