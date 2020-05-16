using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MoneySmart.Data
{
    public static class ApplicationDbSeedData
    {
        private const string AdminUser = "admin@example.com";
        private const string AdminPassword = "Secret123$";

        public static async Task SeedAsync(UserManager<IdentityUser> userManager)
        {
            var user = await userManager.FindByIdAsync(AdminUser);
            if (user == null)
            {
                user = new IdentityUser(AdminUser) { Email = AdminUser };
                await userManager.CreateAsync(user, AdminPassword);
            }
        }
    }
}
