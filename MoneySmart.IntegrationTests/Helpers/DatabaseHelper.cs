using MoneySmart.Data;

namespace MoneySmart.IntegrationTests.Helpers
{
    public static class DatabaseHelper
    {
        public static void InitializeTestDatabase(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();
        }

        public static void ResetTestDatabase(ApplicationDbContext context)
        {
            context.Database.EnsureDeleted();
            InitializeTestDatabase(context);
        }
    }
}
