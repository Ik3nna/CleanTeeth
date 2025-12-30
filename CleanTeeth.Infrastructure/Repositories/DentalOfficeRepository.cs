using CleanTeeth.Domain.Entities;
using CleanTeeth.Domain.Interfaces;
using CleanTeeth.Infrastructure.Data;

namespace CleanTeeth.Infrastructure.Repositories;

public class DentalOfficeRepository : Repository<DentalOffice>, IDentalOfficeRepository
{
    public DentalOfficeRepository(
        CleanTeethDbContext dbContext, 
        IUnitOfWork unitOfWork
    ) : 
    base(dbContext, unitOfWork)
    {
    }
}
