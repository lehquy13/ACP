using ACP.Domain.Business;
using ACP.Domain.Business.Identities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ACP.Infrastructure.Persistence.EntityFrameworkCore;

public class AcpDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;

    public DbSet<IdentityUser> IdentityUsers { get; set; } = null!;

    public DbSet<IdentityRole> IdentityRoles { get; set; } = null!;

    public DbSet<IdentityUserRole> IdentityUserRoles { get; set; } = null!;

    public AcpDbContext(DbContextOptions<AcpDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AcpDbContext).Assembly);
    }
}

//using to support add migration
public class AcpDbContextFactory : IDesignTimeDbContextFactory<AcpDbContext>
{
    public AcpDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AcpDbContext>();

        optionsBuilder.UseSqlServer(
            "Server=(LocalDb)\\MSSQLLocalDB;Database=ACP;Trusted_Connection=True;TrustServerCertificate=True"
        );

        return new AcpDbContext(optionsBuilder.Options);
    }
}