using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneySmart.Domain;

namespace MoneySmart.Data.Configuration;

public class AccountEntityConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("Accounts").HasKey(k => k.Id);
        builder.Property(p => p.Id).HasColumnName("AccountId");
        builder.Property(p => p.Name).HasMaxLength(256).IsRequired().IsUnicode(false);
        builder.Property(p => p.Number).IsRequired();
        builder.HasIndex(p => new { p.Number }, "IX_Accounts_Number").IsUnique();
    }
}
