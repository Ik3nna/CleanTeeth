using CleanTeeth.Domain.Entities;
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
    }
}
