using ACP.Domain.Business;
using ACP.Domain.Business.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ACP.Infrastructure.Persistence.Configs;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id)
            .HasColumnName("Id")
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => IdentityGuid.Create(value)
            );

        builder.Property(r => r.FirstName).IsRequired().HasMaxLength(128);
        builder.Property(r => r.LastName).IsRequired().HasMaxLength(128);
        builder.Property(r => r.Gender).IsRequired();
        builder.Property(r => r.BirthYear).IsRequired();
        builder.Property(r => r.Address).IsRequired().HasMaxLength(128);
        builder.Property(r => r.Description).IsRequired().HasMaxLength(128);
        builder.Property(r => r.Avatar);

        builder.OwnsOne(user => user.Address,
            navigationBuilder =>
            {
                navigationBuilder.Property(address => address.Country)
                    .HasColumnName("Country");
                navigationBuilder.Property(address => address.City)
                    .HasColumnName("City");
            });
    }
}