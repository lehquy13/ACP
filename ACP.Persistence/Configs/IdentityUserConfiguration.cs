using ACP.Domain.Entities.Identities;
using ACP.Domain.Entities.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ACP.Infrastructure.Persistence.Configs;

//TODO: learn how to config with owns many
public class IdentityUserConfiguration : IEntityTypeConfiguration<IdentityUser>
{
    public void Configure(EntityTypeBuilder<IdentityUser> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id)
            .HasColumnName("Id")
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => IdentityGuid.Create(value)
            );

        builder.Property(r => r.PasswordHash).IsRequired();
        builder.Property(r => r.PasswordSalt).IsRequired();

        builder.Property(r => r.UserName).HasMaxLength(128);
        builder.Property(r => r.NormalizedUserName).HasMaxLength(128);

        builder.Property(r => r.PasswordHash).HasMaxLength(128);

        builder.Property(r => r.Email).HasMaxLength(128);
        builder.Property(r => r.NormalizedEmail).HasMaxLength(128);
        builder.Property(r => r.EmailConfirmed);

        builder.Property(r => r.PhoneNumber).HasMaxLength(11);
        builder.Property(r => r.PhoneNumberConfirmed);

        builder.Property(r => r.ConcurrencyStamp).IsRequired();

        //Mark UserId as a foreign key for IdentityUser
        builder.HasOne(r => r.User)
            .WithOne() // TODO: Check does it generate correct SQL query
            .HasForeignKey<IdentityUser>(r => r.Id)
            .IsRequired();

        builder.OwnsOne(r => r.OtpCode,
            navigationBuilder =>
            {
                navigationBuilder.Property(address => address.Value)
                    .HasColumnName("OtpCode")
                    .HasMaxLength(6);
                navigationBuilder.Property(address => address.ExpiredTime)
                    .HasColumnName("ExpiredTime");
            });

        builder.OwnsOne(p => p.IdentityRole);

    }
}