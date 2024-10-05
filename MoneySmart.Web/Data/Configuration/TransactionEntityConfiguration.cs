using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneySmart.Domain;

namespace MoneySmart.Data.Configuration;

public class TransactionEntityConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("Transactions").HasKey(k => k.Id);
        builder.Property(p => p.Id).HasColumnName("TransactionId");
        builder.Property(p => p.DateTime).IsRequired();
        builder.Property(p => p.Description).HasMaxLength(255).IsRequired();
        builder.Property(p => p.TransactionType)
            .HasConversion(type => (string)type, type => (TransactionType)type)
            .HasMaxLength(7);
        builder.Property(p => p.Amount).HasColumnType("decimal(8, 2)").IsRequired();
        builder.Property(p => p.Note).HasColumnType("varchar(255)");
        builder.HasIndex(new[] { "AccountId" }, "IX_Transactions_AccountId");
        builder.HasIndex(p => new { p.DateTime }, "IX_Transactions_DateTime");
        builder.HasOne(p => p.Account).WithMany(p => p.Transactions)
            .HasForeignKey("AccountId").OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(p => p.Transfer).WithMany(p => p.Transactions)
            .HasForeignKey("TransferId").OnDelete(DeleteBehavior.NoAction);
    }
}
