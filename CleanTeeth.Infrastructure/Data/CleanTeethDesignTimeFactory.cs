using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CleanTeeth.Infrastructure.Data
{
    // This factory is used ONLY at design-time by EF Core CLI
    public class CleanTeethDbContextFactory : IDesignTimeDbContextFactory<CleanTeethDbContext>
    {
        public CleanTeethDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CleanTeethDbContext>();
            
            // Use the same connection string you use in Program.cs
            optionsBuilder.UseSqlServer(
                "Server=localhost,1433;Database=CleanTeethDb;User ID=SA;Password=YourStrong!Passw0rd;TrustServerCertificate=True"
            );

            return new CleanTeethDbContext(optionsBuilder.Options);
        }
    }
}
