using System;
using CleanTeeth.Domain.Entities;
using CleanTeeth.Domain.Interfaces;
using CleanTeeth.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CleanTeeth.Infrastructure.Repositories;

public class PatientRepository : IPatientRepository
{
    private readonly CleanTeethDbContext _dbContext;
    private readonly IUnitOfWork _unitOfWork;
    public PatientRepository(CleanTeethDbContext dbContext, IUnitOfWork unitOfWork)
    {
        _dbContext = dbContext;
        _unitOfWork = unitOfWork;
    }
    public async Task<Patient> AddPatientAsync(Patient patient)
    {
        await _dbContext.Patients.AddAsync(patient);
        await _unitOfWork.CommitAsync();
        return patient;
    }

    public async Task DeletePatientAsync(Guid id)
    {
        var patient = await _dbContext.Patients.FindAsync(id);
        if (patient != null)
        {
            _dbContext.Patients.Remove(patient);
        }
        await _unitOfWork.CommitAsync();
    }

    public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
    {
        var patients = await _dbContext.Patients.ToListAsync();
        return patients;
    }

    public async Task<Patient> GetPatientsByIdAsync(Guid id)
    {
        var patient = await _dbContext.Patients.FindAsync(id) ?? throw new KeyNotFoundException($"Patient with ID {id} not found.");
        return patient;
    }

    public async Task<Patient> UpdatePatientAsync(Patient patient)
    {
        _dbContext.Patients.Update(patient);
        await _unitOfWork.CommitAsync();
        return patient;
    }
}
