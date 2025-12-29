using CleanTeeth.Domain.Entities;

namespace CleanTeeth.Domain.Interfaces;

public interface IPatientRepository
{
    Task<Patient> GetPatientsByIdAsync(Guid id);
    Task<IEnumerable<Patient>> GetAllPatientsAsync();
    Task<Patient> AddPatientAsync(Patient patient);
    Task<Patient> UpdatePatientAsync(Patient patient);
    Task DeletePatientAsync(Guid id);
}
