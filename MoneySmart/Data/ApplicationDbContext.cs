using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MoneySmart.Entities;

namespace MoneySmart.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Account>(a =>
            {
                a.ToTable("Accounts").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("AccountId");
                a.Property(p => p.Name).HasMaxLength(255).IsRequired();
                a.Property(p => p.Number).HasMaxLength(255).IsRequired();
                a.HasMany(p => p.Transactions).WithOne(t => t.Account);
            });

            builder.Entity<Transaction>(t =>
            {
                t.ToTable("Transactions").HasKey(k => k.Id);
                t.Property(p => p.Id).HasColumnName("TransactionId");
                t.Property(p => p.DateTime).IsRequired();
                t.Property(p => p.Description).HasMaxLength(255).IsRequired();
                t.Property(p => p.TransactionType).HasConversion<string>().HasMaxLength(7);
                t.Property(p => p.Amount).HasColumnType("decimal(8, 2)").IsRequired();
                t.HasOne(p => p.Account).WithMany(p => p.Transactions);
            });

            base.OnModelCreating(builder);
        }
    }
}
