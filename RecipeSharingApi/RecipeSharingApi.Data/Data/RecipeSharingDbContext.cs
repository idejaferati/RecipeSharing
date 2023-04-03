using Microsoft.EntityFrameworkCore;
using RecipeSharingApi.DataLayer.Models.Entities;
using RecipeSharingApi.DataLayer.Models.Entities.Mappings;

namespace RecipeSharingApi.DataLayer.Data
{
    public class RecipeSharingDbContext : DbContext
    {

        public RecipeSharingDbContext(DbContextOptions<RecipeSharingDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Cuisine> Cuisines { get; set; }
        public DbSet<CookBook> CookBook { get; set; }
        public DbSet<Collection> Collections { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}