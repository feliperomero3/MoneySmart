using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneySmart.Domain;

namespace MoneySmart.Data.Configuration;

public class TransferEntityConfiguration : IEntityTypeConfiguration<Transfer>
{
    public void Configure(EntityTypeBuilder<Transfer> builder)
    {
        builder.ToTable("Transfers").HasKey(k => k.Id);
        builder.Property(p => p.Id).HasColumnName("TransferId");
        builder.Property(p => p.Notes).HasMaxLength(4096);
    }
}
