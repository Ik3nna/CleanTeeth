using CleanTeeth.Domain.Entities;
using CleanTeeth.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace CleanTeeth.Infrastructure.Data;

public class CleanTeethDbContext : DbContext
{
    public CleanTeethDbContext(DbContextOptions<CleanTeethDbContext> options) : base(options)
    {
        
    }

    public DbSet<DentalOffice> DentalOffices { get; set; }

    public DbSet<Patient> Patients { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Dental Office configuration
        modelBuilder.Entity<DentalOffice>(entity =>
        {
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(150);
        });

        // Patient configuration
        modelBuilder.Entity<Patient>(patient =>
        {
            patient.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(150);
            
            // Treat Email as a Value Object
            patient.Property(p => p.Email) // map directly
                .HasConversion(
                    email => email.Value,
                    value => new Email(value))
                .IsRequired()
                .HasMaxLength(256);

            patient.HasIndex(p => p.Email)
                .IsUnique();
        });
    }
}
