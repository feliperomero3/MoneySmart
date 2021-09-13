using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MoneySmart.Entities;

namespace MoneySmart.Data
{
    public static class ApplicationDbSeedData
    {
        private const string AdminUser = "admin@example.com";
        private const string AdminPassword = "Secret123$";

        public static async Task SeedAsync(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            var user = await userManager.FindByEmailAsync(AdminUser);

            if (user == null)
            {
                user = new IdentityUser(AdminUser) { Email = AdminUser };
                await userManager.CreateAsync(user, AdminPassword);
            }

            if (context.Accounts.Any())
            {
                return;
            }

            var account1 = new Account(5221, "Savings");
            var account2 = new Account(7551, "Checking");
            var account3 = new Account(8661, "Money market");

            await context.Accounts.AddRangeAsync(account1, account2, account3);

            var transaction1 = new Transaction(DateTime.Parse("2020-08-08T10:00:00"), account1, "First Deposit",
                TransactionType.Income, 1000);

            var transaction2 = new Transaction(DateTime.Parse("2020-08-08T10:00:00"), account2, "First Deposit (C)",
                TransactionType.Income, 10000);

            var transaction3 = new Transaction(DateTime.Parse("2020-08-10T12:00:00"), account1, "Groceries",
                TransactionType.Expense, 64);

            var transaction4 = new Transaction(DateTime.Parse("2020-08-15T16:00:00"), account2, "Car Payment",
                TransactionType.Expense, 250);

            await context.Transactions.AddRangeAsync(transaction1, transaction2, transaction3, transaction4);

            await context.SaveChangesAsync();
        }
    }
}
