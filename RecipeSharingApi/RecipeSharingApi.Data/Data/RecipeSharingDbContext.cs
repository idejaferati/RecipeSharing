using Microsoft.EntityFrameworkCore;
namespace RecipeSharingApi.DataLayer.Data
{
    public class RecipeSharingDbContext : DbContext
    {

        public RecipeSharingDbContext(DbContextOptions<RecipeSharingDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}