using System;
using MoneySmart.Data;
using MoneySmart.Domain;

namespace MoneySmart.IntegrationTests.Helpers
{
    public static class DatabaseHelper
    {
        private static readonly object Lock = new();
        private static bool _databaseInitialized;

        public static void InitializeTestDatabase(ApplicationDbContext context)
        {
            lock (Lock)
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
            var user = new Microsoft.AspNetCore.Identity.IdentityUser
            {
                UserName = "admin@example.com",
                NormalizedUserName = "ADMIN@EXAMPLE.COM",
                Email = "admin@example.com",
                NormalizedEmail = "ADMIN@EXAMPLE.COM"
            };

            var passwordHasher = new Microsoft.AspNetCore.Identity.PasswordHasher<Microsoft.AspNetCore.Identity.IdentityUser>();
            user.PasswordHash = passwordHasher.HashPassword(user, "Secret123$");

            context.Users.Add(user);

            var account1 = new Account(5221, "Savings");
            var account2 = new Account(2152, "Expenses");
            var account3 = new Account(9999, "Throwaway");

            var transaction1 = new Transaction(DateTime.Parse("2020-08-08T10:00:00"), account1, "First Deposit",
                TransactionType.Income, 1000);

            context.Accounts.AddRange(account1, account2, account3);
            context.Transactions.Add(transaction1);

            context.SaveChanges();
        }

        public static void ResetTestDatabase(ApplicationDbContext context)
        {
            InitializeTestDatabase(context);
        }
    }
}
