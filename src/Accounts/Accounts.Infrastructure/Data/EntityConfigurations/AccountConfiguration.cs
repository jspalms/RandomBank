namespace Accounts.Infrastructure.Data.EntityConfigurations;

using Domain.Entities;
using Domain.Entities.Accounts;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class AccountConfiguration: IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasDiscriminator<AccountType>(x => x.AccountType)
            .HasValue<FixedISAAccount>(AccountType.FixedSavingsAccount)
            .HasValue<VariableISAAccount>(AccountType.VariableSavingsAccount);
    }
}