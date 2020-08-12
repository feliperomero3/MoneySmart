using Microsoft.EntityFrameworkCore;

namespace MoneySmart.Data
{
    public static class ApplicationDbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.Migrate();
        }
    }
}
