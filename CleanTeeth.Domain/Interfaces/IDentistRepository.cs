using CleanTeeth.Domain.Entities;

namespace CleanTeeth.Domain.Interfaces;

public interface IDentistRepository : IRepository<Dentist>
{
    Task<PagedResult<Dentist>> GetFilteredAsync(
        int page,
        int pageSize,
        string? name,
        string? email
    );
}
