using System;
using MoneySmart.Data;
using MoneySmart.Entities;

namespace MoneySmart.IntegrationTests.Helpers
{
    public static class DatabaseHelper
    {
        public static void InitializeTestDatabase(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();
        }

        public static void SeedTestDatabase(ApplicationDbContext context)
        {
            var account1 = new Account("5221", "Savings");

            var transaction1 = new Transaction(DateTime.Parse("2020-08-08T10:00:00"), account1, "First Deposit",
                TransactionType.Income, 1000);

            context.Accounts.Add(account1);
            context.Transactions.Add(transaction1);

            context.SaveChanges();
        }

        public static void ResetTestDatabase(ApplicationDbContext context)
        {
            context.Database.EnsureDeleted();
            InitializeTestDatabase(context);
        }
    }
}
