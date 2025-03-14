using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MoneySmart.Domain;

namespace MoneySmart.Data
{
    [ExcludeFromCodeCoverage]
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

            var accounts = new[]
            {
                new Account(5221, "Savings", user.Id),
                new Account(7551, "Checking", user.Id),
                new Account(8661, "Money Market", user.Id),
                new Account(9009, "Business", user.Id)
            };

            await context.Accounts.AddRangeAsync(accounts);

            var now = DateTime.Now;

            var transactions = new[]
            {
                new Transaction(now - TimeSpan.FromMinutes(90), accounts[0], "First Deposit", TransactionType.Income, 1000),
                new Transaction(now - TimeSpan.FromMinutes(60), accounts[1], "First Deposit (C)", TransactionType.Income, 10000),
                new Transaction(now - TimeSpan.FromMinutes(30), accounts[1], "House Maintenance", TransactionType.Expense, 900),
                new Transaction(now - TimeSpan.FromMinutes(16), accounts[3], "Car Payment", TransactionType.Expense, 1500),
                new Transaction(now - TimeSpan.FromMinutes(12), accounts[3], "Withdrawal", TransactionType.Expense, 600)
            };

            await context.Transactions.AddRangeAsync(transactions);

            await context.SaveChangesAsync();
        }
    }
}
