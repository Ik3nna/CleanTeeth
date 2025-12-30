using CleanTeeth.Domain.Entities;

namespace CleanTeeth.Domain.Interfaces;

public interface IPatientRepository : IRepository<Patient>
{
    Task<PagedResult<Patient>> GetFilteredAsync(
        int page,
        int pageSize,
        string? name,
        string? email
    );
}
