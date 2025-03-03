namespace Accounts.Infrastructure.Data.EntityConfigurations;

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserPortfolioConfiguration : IEntityTypeConfiguration<UserPortfolio>
{
    public void Configure(EntityTypeBuilder<UserPortfolio> builder)
    {
        builder.Property("VersionNumber").IsConcurrencyToken();
        builder.OwnsOne(p => p.UserISALimits);
    }
}