using System;
using Microsoft.AspNetCore.Identity;
using MoneySmart.Data;
using MoneySmart.Domain;

namespace MoneySmart.IntegrationTests.Helpers
{
    public static class DatabaseHelper
    {
        private static readonly object Lock = new();
        private static bool _databaseInitialized;

        private const string AdminUser = "admin@example.com";
        private const string AdminPassword = "Secret123$";

        public static void InitializeTestDatabase(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            lock (Lock)
            {
                if (!_databaseInitialized)
                {
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();

                    SeedTestDatabase(context, userManager);

                    _databaseInitialized = true;
                }
            }
        }

        private static void SeedTestDatabase(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            var user = new IdentityUser(AdminUser) { Email = AdminUser };
            userManager.CreateAsync(user, AdminPassword).GetAwaiter().GetResult();

            var account1 = new Account(5221, "Savings");
            var account2 = new Account(2152, "Expenses");
            var account3 = new Account(9999, "Throwaway");

            var transaction1 = new Transaction(DateTime.Parse("2020-08-08T10:00:00"), account1, "First Deposit",
                TransactionType.Income, 1000);

            context.Accounts.AddRange(account1, account2, account3);
            context.Transactions.Add(transaction1);

            context.SaveChanges();
        }

        public static void ResetTestDatabase(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            InitializeTestDatabase(context, userManager);
        }
    }
}
