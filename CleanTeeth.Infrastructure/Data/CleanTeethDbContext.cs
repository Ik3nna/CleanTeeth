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
    public DbSet<Dentist> Dentists { get; set; }
    public DbSet<Appointment> Appointments { get; set; }

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

        // Dentist configuration
        modelBuilder.Entity<Dentist>(dentist =>
        {
            dentist.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(150);
            
            // Treat Email as a Value Object
            dentist.Property(p => p.Email) // map directly
                .HasConversion(
                    email => email.Value,
                    value => new Email(value))
                .IsRequired()
                .HasMaxLength(256);

            dentist.HasIndex(p => p.Email)
                .IsUnique();
        });

        // Appointment Configuration
        modelBuilder.Entity<Appointment>(appointment =>
        {
            appointment.ComplexProperty(p => p.TimeInterval, ti =>
            {
                ti.Property(p => p.StartTime).HasColumnName("StartDate");
                ti.Property(p => p.EndTime).HasColumnName("EndDate");
            });
        });
    }
}
