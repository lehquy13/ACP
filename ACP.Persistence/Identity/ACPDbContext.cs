using ACP.Domain.Entities;
using ACP.Domain.Entities.Identities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ACP.Infrastructure.Persistence.Identity;

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

        modelBuilder.Entity<IdentityUser>(re =>
        {
            re.ToTable("IdentityUser");
            re.HasKey(r => r.Id);
            
            re.Property(r => r.UserName).IsRequired().HasMaxLength(128);
            re.Property(r => r.NormalizedUserName).IsRequired().HasMaxLength(128);

            re.Property(r => r.PasswordHash).IsRequired().HasMaxLength(128);

            re.Property(r => r.Email).IsRequired().HasMaxLength(128);
            re.Property(r => r.NormalizedEmail).IsRequired().HasMaxLength(128);
            re.Property(r => r.EmailConfirmed).IsRequired();

            re.Property(r => r.PhoneNumber).IsRequired().HasMaxLength(11);
            re.Property(r => r.PhoneNumberConfirmed).IsRequired();

            re.Property(r => r.ConcurrencyStamp).IsRequired();

            re.HasOne<IdentityRole>().WithMany().HasForeignKey(r => r.IdentityRoleId);
        });

        modelBuilder.Entity<IdentityRole>(re =>
        {
            re.ToTable("IdentityRole");
            re.HasKey(r => r.Id);
            re.Property(r => r.Name).IsRequired().HasMaxLength(128);
        });

        modelBuilder.Entity<User>(re =>
        {
            re.ToTable("User");
            re.HasKey(r => r.Id);

            re.Property(r => r.FirstName).IsRequired().HasMaxLength(128);
            re.Property(r => r.LastName).IsRequired().HasMaxLength(128);
            re.Property(r => r.Gender).IsRequired();
            re.Property(r => r.BirthYear).IsRequired();
            re.Property(r => r.Address).IsRequired().HasMaxLength(128);
            re.Property(r => r.Description).IsRequired().HasMaxLength(128);
            re.Property(r => r.Avatar).IsRequired();
        });
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