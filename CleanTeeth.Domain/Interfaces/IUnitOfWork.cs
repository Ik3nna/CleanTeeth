namespace CleanTeeth.Domain.Interfaces;

public interface IUnitOfWork
{
    Task CommitAsync();
    Task RollbackAsync();
}
