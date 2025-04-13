using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Domain.Entities;

namespace Users.Infrastructure.Data.EntityConfiguration;

class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property("VersionNumber").IsConcurrencyToken();
        builder.OwnsOne(u => u.Email, email =>
        {
            email
                .Property(e => e.Value)
                .HasColumnName("Email")
                .IsRequired();
        });
        builder.HasIndex(u => u.DeletedAt);
        builder.HasQueryFilter(x => x.DeletedAt == null);
    }
}


