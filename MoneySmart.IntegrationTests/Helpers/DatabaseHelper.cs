using System;
using MoneySmart.Data;
using MoneySmart.Entities;

namespace MoneySmart.IntegrationTests.Helpers
{
    public static class DatabaseHelper
    {
        private static readonly object _lock = new object();
        private static bool _databaseInitialized;

        public static void InitializeTestDatabase(ApplicationDbContext context)
        {
            lock (_lock)
            {
                if (!_databaseInitialized)
                {
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();

                    SeedTestDatabase(context);

                    _databaseInitialized = true;
                }
            }
        }

        private static void SeedTestDatabase(ApplicationDbContext context)
        {
            var account1 = new Account("5221", "Savings");
            var account2 = new Account("2152", "Expenses");

            var transaction1 = new Transaction(DateTime.Parse("2020-08-08T10:00:00"), account1, "First Deposit",
                TransactionType.Income, 1000);

            context.Accounts.Add(account1);
            context.Accounts.Add(account2);
            context.Transactions.Add(transaction1);

            context.SaveChanges();
        }

        public static void ResetTestDatabase(ApplicationDbContext context)
        {
            InitializeTestDatabase(context);
        }
    }
}
