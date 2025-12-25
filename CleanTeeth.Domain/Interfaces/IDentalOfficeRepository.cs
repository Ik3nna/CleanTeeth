using CleanTeeth.Domain.Entities;

namespace CleanTeeth.Domain.Interfaces;

public interface IDentalOfficeRepository
{
    Task<DentalOffice> GetDentalOfficeByIdAsync(Guid id);
    Task<IEnumerable<DentalOffice>> GetAllDentalOfficesAsync();
    Task<DentalOffice> AddDentalOfficeAsync(DentalOffice dentalOffice);
    Task<DentalOffice> UpdateDentalOfficeAsync(DentalOffice dentalOffice);
    Task DeleteDentalOfficeAsync(Guid id);
}
