using CleanTeeth.Domain.Entities;
using CleanTeeth.Domain.Interfaces;
using CleanTeeth.Infrastructure.Data;

namespace CleanTeeth.Infrastructure.Repositories;

public class PatientRepository : Repository<Patient>, IPatientRepository
{
    public PatientRepository(
        CleanTeethDbContext dbContext, 
        IUnitOfWork unitOfWork
    ) : base(dbContext, unitOfWork)
    {
    }
}
