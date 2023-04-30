using Microsoft.EntityFrameworkCore;
using RecipeSharingApi.DataLayer.Data.EntityConfigurations;
using RecipeSharingApi.DataLayer.Models.DTOs;
using RecipeSharingApi.DataLayer.Models.Entities;
using RecipeSharingApi.DataLayer.Models.Entities.Mappings;

namespace RecipeSharingApi.DataLayer.Data
{
    public class RecipeSharingDbContext : DbContext
    {
        
        public RecipeSharingDbContext(DbContextOptions options) : base(options) { }



        public DbSet<UserCreateDTO> Users { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Cuisine> Cuisines { get; set; }
        public DbSet<CookBook> CookBook { get; set; }
        public DbSet<Collection> Collections { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Collection>().HasMany(e => e.Recipes).WithMany(e => e.Collections);
            modelBuilder.Entity<Recipe>().Property(r => r.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Recipe>().HasOne(u => u.User).WithMany(r => r.Recipes)
                        .HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Recipe>().HasMany(r => r.Instructions).WithOne(r => r.Recipe)
                        .HasForeignKey(r => r.RecipeId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Recipe>().HasMany(r => r.Ingredients).WithOne(r => r.Recipe)
                        .HasForeignKey(r => r.RecipeId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Recipe>().HasMany(t => t.Tags).WithMany(r => r.Recipes);
            modelBuilder.Entity<Recipe>().HasOne(c => c.CookBook).WithMany(r => r.Recipes);
            modelBuilder.Entity<CookBook>().HasMany(r => r.Recipes).WithOne(c => c.CookBook).OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Tag>().Property(t => t.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Tag>().HasIndex(t => t.Name).IsUnique();

            modelBuilder.Entity<CookBook>().Property(c => c.Id).ValueGeneratedOnAdd();

            // TODO: What to do with the recipes when the user is deleted?
            modelBuilder.Entity<User>().HasMany(r => r.Recipes).WithOne(u => u.User)
                        .HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}