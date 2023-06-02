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



        public DbSet<User> Users { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Cuisine> Cuisines { get; set; }
        public DbSet<CookBook> CookBook { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Policy> Policies { get; set; }
        public DbSet<PolicyRole> PolicyRoles { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<RecommendationScore> RecommendationScore { get; set; }
        public DbSet<ShoppingListItem> ShoppingListItem { get; set; }

        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
        public DbSet<RecipeInstruction> InstructionSteps { get; set; }

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

            modelBuilder.Entity<RecommendationScore>().Property(r => r.Id).ValueGeneratedOnAdd();


            modelBuilder.Entity<CookBook>().Property(c => c.Id).ValueGeneratedOnAdd();

            // TODO: What to do with the recipes when the user is deleted?
            modelBuilder.Entity<User>().HasMany(r => r.Recipes).WithOne(u => u.User)
                        .HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>().HasOne(u => u.Role).WithMany().HasForeignKey(u => u.RoleId);

            modelBuilder.Entity<PolicyRole>()
            .HasOne(u => u.Roles)
            .WithMany()
            .HasForeignKey(u => u.RoleId);

            modelBuilder.Entity<PolicyRole>()
           .HasOne(u => u.Policies)
           .WithMany()
           .HasForeignKey(u => u.PolicyId);

            modelBuilder.Entity<Review>().Property(i => i.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Review>().HasKey(k => k.Id);
            modelBuilder.Entity<Review>().HasOne(u => u.User).WithMany(r => r.Reviews)
                .HasForeignKey(u => u.UserId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ShoppingListItem>().HasKey(e => new { e.Id, e.UserId, e.Name });
            modelBuilder.Entity<ShoppingListItem>().Property(r => r.Id).ValueGeneratedOnAdd();


            modelBuilder.Entity<Policy>().HasData(
     new Policy { Id = new Guid("2bc75a12-6655-441c-af9d-32be58a5531e"), Name = "GetNameOfUser" },
     new Policy { Id = new Guid("6d966a93-0aa1-420b-bf37-d7288e8e638e"), Name = "addrole" },
     new Policy { Id = new Guid("68d3008d-ede9-4ac8-8eb9-0f8877358476"), Name = "addPolicy" },
     new Policy { Id = new Guid("1381225f-387b-49c9-b57a-6a1a379fdb8a"), Name = "addRolePolicy" },
     new Policy { Id = new Guid("410595d2-43e0-4844-b672-a59a4151d7d5"), Name = "getRoles" },
     new Policy { Id = new Guid("e00b12ee-cde2-44e3-ba10-ef4b45390252"), Name = "getPolicies" },
     new Policy { Id = new Guid("0e8e465e-8abd-4c2f-a6a3-3796b0d702bc"), Name = "PostCollections" },
     new Policy { Id = new Guid("d0f9bb82-9abe-4841-ab56-dd79cff023d3"), Name = "getCollections" },
     new Policy { Id = new Guid("ad057d33-b1ab-491f-a143-a7cd45399a7b"), Name = "getCollectionsById" },
     new Policy { Id = new Guid("ae6e7487-2375-4e33-911c-74ca4d375f5a"), Name = "putCollections" },
     new Policy { Id = new Guid("3b90b27c-5690-4305-a418-3f58efeade35"), Name = "getUserCollections" },
     new Policy { Id = new Guid("f7d9d48f-62c4-44eb-9ac8-1e7ce066c88d"), Name = "deleteCollections" },
     new Policy { Id = new Guid("97fa4a01-82c8-4ea2-addc-cc5f4037dc1e"), Name = "postRecipeInCollections" },
     new Policy { Id = new Guid("2b0088ca-6ee6-41a7-b931-ee61f786bd55"), Name = "RemoveRecipeCollections" },
     new Policy { Id = new Guid("cefaeb1f-7ca5-41dd-bd1c-e0b66f5f6051"), Name = "PostCookbooks" },
     new Policy { Id = new Guid("6929858c-31ad-40b0-b53d-bc0cc30548ab"), Name = "AllCookbooks" },
     new Policy { Id = new Guid("6f563976-863c-409d-8237-f08ed043ffcd"), Name = "AllUserCookbooks" },
     new Policy { Id = new Guid("257400bf-1dbf-4f30-83d7-d1f482651a8d"), Name = "GetCookbookById" },
     new Policy { Id = new Guid("71c436da-3849-4459-8f26-7b299c74162f"), Name = "AllCookbooksByPage" },
     new Policy { Id = new Guid("1f1260ac-260e-4bc8-8cb3-a7fa5d8dc366"), Name = "UpdateCookBook" },
     new Policy { Id = new Guid("bf9f8380-023f-4660-9c0f-f8fce5295f48"), Name = "DeleteCookbook" },
     new Policy { Id = new Guid("8460422a-5e83-4d24-82a2-eeb45b902406"), Name = "AddRecipeOnCookbook" },
     new Policy { Id = new Guid("d787eb55-0b45-45c6-a6fb-0566209831b0"), Name = "DeleteRecipeFromCookbook" },
     new Policy { Id = new Guid("403015ab-9012-42d8-b13b-65366085303a"), Name = "CreateCuisine" },
     new Policy { Id = new Guid("1d77b3dd-fd7b-4db6-b528-a5c9470378dd"), Name = "UpdateCuisine" },
     new Policy { Id = new Guid("ec67c7c7-653d-4078-b9ec-82f9d188fd69"), Name = "DeleteCuisine" },
     new Policy { Id = new Guid("ccb9a20c-6dc2-4c37-a792-33790c42e256"), Name = "CreateRecipe" },
     new Policy { Id = new Guid("2844b8fb-a338-4801-b06b-6f80d923d6c2"), Name = "UpdateRecipe" },
     new Policy { Id = new Guid("8884e0bf-6477-4efc-b5e2-a34fe1d1dda5"), Name = "GetRecipeByUserId" },
     new Policy { Id = new Guid("d6a80b7d-70f3-4a63-a1a4-7ff70c110a49"), Name = "GetAllUserRecipe" },
     new Policy { Id = new Guid("e73cf4bb-4a96-49c2-a3ef-428915e2f968"), Name = "DeleteRecipe" },
     new Policy { Id = new Guid("a9401ffb-8d6a-47cd-b9ba-d5d740bb86f8"), Name = "ReviewRecipe" },
     new Policy { Id = new Guid("0c19f0c5-3da9-435f-b784-81482e074738"), Name = "UpdateReviews" },
     new Policy { Id = new Guid("e8fbc57b-4837-4d73-be89-e9f6ce4dc328"), Name = "DelteRewiews" },
     new Policy { Id = new Guid("d5662e2c-f6db-4601-8911-a3faf99d03d2"), Name = "CreateShopingList" },
     new Policy { Id = new Guid("ea13f8d7-a3ed-4464-bfbb-f4af8db2b567"), Name = "DeleteItemFromShopingList" },
     new Policy { Id = new Guid("c3001055-9124-4aea-a1ac-2dccb4257512"), Name = "GetItemFromShopingListById" },
     new Policy { Id = new Guid("8f69170f-064c-40b2-804e-c4b53bb3218d"), Name = "GetLink" },
     new Policy { Id = new Guid("65ab361d-916d-43cd-816f-49ed8688bf87"), Name = "GetUserShopingList" },
     new Policy { Id = new Guid("a80c3363-4977-49f5-9327-1c9b76baa3ed"), Name = "GetAllTags" },
     new Policy { Id = new Guid("0d423b1a-3bc6-48f3-bc9c-c1bb8d5a7071"), Name = "GetATagById" },
     new Policy { Id = new Guid("3eb0b264-294e-4a1d-a748-5610d2a1f1f1"), Name = "CreateATag" },
     new Policy { Id = new Guid("5d5a00cb-4c9c-4950-aace-8dd956eaec0c"), Name = "UpdateTag" },
     new Policy { Id = new Guid("1b68dcb1-d256-4d9e-93f7-51008c268cb9"), Name = "DeleteTag" },
     new Policy { Id = new Guid("0fce3dfd-befa-47f7-8a66-07964b6d8f4d"), Name = "DeleteUser" },
     new Policy { Id = new Guid("d2194a88-8cb5-41c3-b40d-227ea502c1a8"), Name = "AllUsers" },
     new Policy { Id = new Guid("58b74ad0-74c8-4909-b79b-60d31a57e49b"), Name = "searchByEmail" },
     new Policy { Id = new Guid("43d2a293-4093-42fd-b80e-c146f5890076"), Name = "ChangePassword" },
     new Policy { Id = new Guid("384a15ef-8a8f-4152-8c3c-196749f9ab72"), Name = "GetUserById" },
     new Policy { Id = new Guid("9b0a1cbc-bb03-48c7-89dd-24b622e6cb84"), Name = "UpdateUser" },
     new Policy { Id = new Guid("2c497d51-de9d-41a7-b23b-c4f51f6dd9d6"), Name = "UpdateUserRole" },
     new Policy { Id = new Guid("3d041ace-b9ef-425f-b520-136e2dc8c5ba"), Name = "MyData" }
 );

            modelBuilder.Entity<Role>().HasData(
            new Role { Id = new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd"), Name = "admin" },
            new Role { Id = new Guid("24bade73-46d0-40dd-95e7-42352504fe2d"), Name = "user" },
            new Role { Id = new Guid("ed263967-56fd-451c-a997-952d26cf7aff"), Name = "editor" }
);
            modelBuilder.Entity<PolicyRole>().HasData(
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("2bc75a12-6655-441c-af9d-32be58a5531e"), RoleId = new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("6d966a93-0aa1-420b-bf37-d7288e8e638e"), RoleId = new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("68d3008d-ede9-4ac8-8eb9-0f8877358476"), RoleId = new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("1381225f-387b-49c9-b57a-6a1a379fdb8a"), RoleId = new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("410595d2-43e0-4844-b672-a59a4151d7d5"), RoleId = new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("e00b12ee-cde2-44e3-ba10-ef4b45390252"), RoleId = new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("403015ab-9012-42d8-b13b-65366085303a"), RoleId = new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("1d77b3dd-fd7b-4db6-b528-a5c9470378dd"), RoleId = new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("ec67c7c7-653d-4078-b9ec-82f9d188fd69"), RoleId = new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("8884e0bf-6477-4efc-b5e2-a34fe1d1dda5"), RoleId = new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("0fce3dfd-befa-47f7-8a66-07964b6d8f4d"), RoleId = new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("d2194a88-8cb5-41c3-b40d-227ea502c1a8"), RoleId = new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("58b74ad0-74c8-4909-b79b-60d31a57e49b"), RoleId = new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },

    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("d0f9bb82-9abe-4841-ab56-dd79cff023d3"), RoleId = new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("cefaeb1f-7ca5-41dd-bd1c-e0b66f5f6051"), RoleId = new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("6929858c-31ad-40b0-b53d-bc0cc30548ab"), RoleId = new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("6f563976-863c-409d-8237-f08ed043ffcd"), RoleId = new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("257400bf-1dbf-4f30-83d7-d1f482651a8d"), RoleId = new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("71c436da-3849-4459-8f26-7b299c74162f"), RoleId = new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("1f1260ac-260e-4bc8-8cb3-a7fa5d8dc366"), RoleId = new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("bf9f8380-023f-4660-9c0f-f8fce5295f48"), RoleId = new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("8460422a-5e83-4d24-82a2-eeb45b902406"), RoleId = new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("d787eb55-0b45-45c6-a6fb-0566209831b0"), RoleId = new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("ccb9a20c-6dc2-4c37-a792-33790c42e256"), RoleId = new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("2844b8fb-a338-4801-b06b-6f80d923d6c2"), RoleId = new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("d6a80b7d-70f3-4a63-a1a4-7ff70c110a49"), RoleId = new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("e8fbc57b-4837-4d73-be89-e9f6ce4dc328"), RoleId = new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("d5662e2c-f6db-4601-8911-a3faf99d03d2"), RoleId = new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },

    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("ea13f8d7-a3ed-4464-bfbb-f4af8db2b567"), RoleId = new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("c3001055-9124-4aea-a1ac-2dccb4257512"), RoleId = new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },

    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("a80c3363-4977-49f5-9327-1c9b76baa3ed"), RoleId = new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("0d423b1a-3bc6-48f3-bc9c-c1bb8d5a7071"), RoleId = new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("3eb0b264-294e-4a1d-a748-5610d2a1f1f1"), RoleId = new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("5d5a00cb-4c9c-4950-aace-8dd956eaec0c"), RoleId = new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("1b68dcb1-d256-4d9e-93f7-51008c268cb9"), RoleId = new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("0fce3dfd-befa-47f7-8a66-07964b6d8f4d"), RoleId = new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("d2194a88-8cb5-41c3-b40d-227ea502c1a8"), RoleId = new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("43d2a293-4093-42fd-b80e-c146f5890076"), RoleId = new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("384a15ef-8a8f-4152-8c3c-196749f9ab72"), RoleId = new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("9b0a1cbc-bb03-48c7-89dd-24b622e6cb84"), RoleId = new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("2c497d51-de9d-41a7-b23b-c4f51f6dd9d6"), RoleId = new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("3d041ace-b9ef-425f-b520-136e2dc8c5ba"), RoleId = new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("a9401ffb-8d6a-47cd-b9ba-d5d740bb86f8"), RoleId = new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("0c19f0c5-3da9-435f-b784-81482e074738"), RoleId = new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("e8fbc57b-4837-4d73-be89-e9f6ce4dc328"), RoleId = new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("d5662e2c-f6db-4601-8911-a3faf99d03d2"), RoleId = new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("ea13f8d7-a3ed-4464-bfbb-f4af8db2b567"), RoleId = new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("c3001055-9124-4aea-a1ac-2dccb4257512"), RoleId = new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("a80c3363-4977-49f5-9327-1c9b76baa3ed"), RoleId = new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("0d423b1a-3bc6-48f3-bc9c-c1bb8d5a7071"), RoleId = new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("3eb0b264-294e-4a1d-a748-5610d2a1f1f1"), RoleId = new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("5d5a00cb-4c9c-4950-aace-8dd956eaec0c"), RoleId = new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("1b68dcb1-d256-4d9e-93f7-51008c268cb9"), RoleId = new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("0fce3dfd-befa-47f7-8a66-07964b6d8f4d"), RoleId = new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("d2194a88-8cb5-41c3-b40d-227ea502c1a8"), RoleId = new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("43d2a293-4093-42fd-b80e-c146f5890076"), RoleId = new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("384a15ef-8a8f-4152-8c3c-196749f9ab72"), RoleId = new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("9b0a1cbc-bb03-48c7-89dd-24b622e6cb84"), RoleId = new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("2c497d51-de9d-41a7-b23b-c4f51f6dd9d6"), RoleId = new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("3d041ace-b9ef-425f-b520-136e2dc8c5ba"), RoleId = new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("e73cf4bb-4a96-49c2-a3ef-428915e2f968"), RoleId = new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("0e8e465e-8abd-4c2f-a6a3-3796b0d702bc"), RoleId = new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("d0f9bb82-9abe-4841-ab56-dd79cff023d3"), RoleId = new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("ad057d33-b1ab-491f-a143-a7cd45399a7b"), RoleId = new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("ae6e7487-2375-4e33-911c-74ca4d375f5a"), RoleId = new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("3b90b27c-5690-4305-a418-3f58efeade35"), RoleId = new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("f7d9d48f-62c4-44eb-9ac8-1e7ce066c88d"), RoleId = new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("97fa4a01-82c8-4ea2-addc-cc5f4037dc1e"), RoleId = new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("2b0088ca-6ee6-41a7-b931-ee61f786bd55"), RoleId = new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("71c436da-3849-4459-8f26-7b299c74162f"), RoleId = new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("bf9f8380-023f-4660-9c0f-f8fce5295f48"), RoleId = new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("cefaeb1f-7ca5-41dd-bd1c-e0b66f5f6051"), RoleId = new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("6929858c-31ad-40b0-b53d-bc0cc30548ab"), RoleId = new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("6f563976-863c-409d-8237-f08ed043ffcd"), RoleId = new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("257400bf-1dbf-4f30-83d7-d1f482651a8d"), RoleId = new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("71c436da-3849-4459-8f26-7b299c74162f"), RoleId = new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("1f1260ac-260e-4bc8-8cb3-a7fa5d8dc366"), RoleId = new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("8460422a-5e83-4d24-82a2-eeb45b902406"), RoleId = new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("d787eb55-0b45-45c6-a6fb-0566209831b0"), RoleId = new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("ccb9a20c-6dc2-4c37-a792-33790c42e256"), RoleId = new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("2844b8fb-a338-4801-b06b-6f80d923d6c2"), RoleId = new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("d6a80b7d-70f3-4a63-a1a4-7ff70c110a49"), RoleId = new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("e73cf4bb-4a96-49c2-a3ef-428915e2f968"), RoleId = new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("a9401ffb-8d6a-47cd-b9ba-d5d740bb86f8"), RoleId = new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("0c19f0c5-3da9-435f-b784-81482e074738"), RoleId = new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("e8fbc57b-4837-4d73-be89-e9f6ce4dc328"), RoleId = new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("d5662e2c-f6db-4601-8911-a3faf99d03d2"), RoleId = new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("ea13f8d7-a3ed-4464-bfbb-f4af8db2b567"), RoleId = new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("c3001055-9124-4aea-a1ac-2dccb4257512"), RoleId = new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("8f69170f-064c-40b2-804e-c4b53bb3218d"), RoleId = new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("65ab361d-916d-43cd-816f-49ed8688bf87"), RoleId = new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("a80c3363-4977-49f5-9327-1c9b76baa3ed"), RoleId = new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("0d423b1a-3bc6-48f3-bc9c-c1bb8d5a7071"), RoleId = new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("3eb0b264-294e-4a1d-a748-5610d2a1f1f1"), RoleId = new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("5d5a00cb-4c9c-4950-aace-8dd956eaec0c"), RoleId = new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("43d2a293-4093-42fd-b80e-c146f5890076"), RoleId = new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("3d041ace-b9ef-425f-b520-136e2dc8c5ba"), RoleId = new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
    new PolicyRole { Id = Guid.NewGuid(), PolicyId = new Guid("9b0a1cbc-bb03-48c7-89dd-24b622e6cb84"), RoleId = new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") }
);



            modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = new Guid("1f19e7bd-c9ff-4fd6-8a1d-5e242c5794c4"), // Assuming you have an Id property in the User class
                FirstName = "Admin",
                LastName = "User",
                Gender = "Male",
                Email = "admin@gmail.com",
                RoleId = new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd"),
                PhoneNumber = "123456789",
                SaltedHashPassword = "$2a$11$dOev5hXKaHxBdLbLrjmgUOX.yeQJdcw2KnBqYjBCIMRaKScfNqvnq",
                Salt = "$2a$11$dOev5hXKaHxBdLbLrjmgUO"
                // Set other properties accordingly
            },
            new User
            {
                Id = new Guid("3e80e914-e301-4cb2-9290-206dcefe364c"), // Assuming you have an Id property in the User class
                FirstName = "Regular",
                LastName = "User",
                Gender = "Female",
                Email = "user@example.com",
                RoleId = new Guid("24bade73-46d0-40dd-95e7-42352504fe2d"),
                PhoneNumber = "987654321",
                SaltedHashPassword = "$2a$11$mISI/pB5Z0LkjBdG3DbZl.WGOh5B86pbsEW6tWHdS6OKZscYgt8X.",
                Salt = "$2a$11$mISI/pB5Z0LkjBdG3DbZl."
            }
        );


            modelBuilder.Entity<Tag>().HasData(
            new Tag { Id = new Guid("55fdbfbc-2f46-4b10-83b2-0d4dd6aa54c3"), Name = "#IceCream" },
            new Tag { Id = new Guid("d4977b4a-8679-4350-95d4-baf729080eae"), Name = "#delicious" },
            new Tag { Id = new Guid("10386470-5c03-495d-8b43-51437069008e"), Name = "#perfect" }
        );


            modelBuilder.Entity<Cuisine>().HasData(
             new Cuisine { Id = new Guid("ca79e18d-2f4a-4d3e-8c7d-5b516b9eb516"), Name = "Germany" },
             new Cuisine { Id = new Guid("f3114be0-104b-463e-9f40-66f5450b0eb1"), Name = "United Kingdom" },
             new Cuisine { Id = new Guid("d89543a3-0a4a-4830-8092-63c77b049a8f"), Name = "USA" }
        );

            modelBuilder.Entity<Recipe>().HasData(
            new Recipe
            {
                Id = new Guid("0c98d5db-4983-40cd-923a-df9135ed467e"), // Assuming you have an Id property in the Recipe class
                UserId = new Guid("3e80e914-e301-4cb2-9290-206dcefe364c"), // Set the UserId for the recipe
                Name = "Steak Recipe",
                Description = "Delicious steak recipe",
                CuisineId = new Guid("d89543a3-0a4a-4830-8092-63c77b049a8f"), // Assuming you have the CuisineId for Steak Cuisine
                PrepTime = 15,
                CookTime = 20,
                Servings = 2,
                Yield = 2,
                Calories = 500,
                AudioInstructions = "https://iamafoodblog.b-cdn.net/wp-content/uploads/2021/02/how-to-cook-steak-1061w.jpg",
                VideoInstructions = "Not available",
                CookBook = null,
                Collections = null
            }
        );

            modelBuilder.Entity<RecipeIngredient>().HasData(
           new RecipeIngredient { Id = Guid.NewGuid(), RecipeId = new Guid("0c98d5db-4983-40cd-923a-df9135ed467e"), Name = "Steak" , Amount = 1 , Unit = Models.Enums.Unit.Pieces},
           new RecipeIngredient { Id = Guid.NewGuid(), RecipeId = new Guid("0c98d5db-4983-40cd-923a-df9135ed467e"), Name = "Salt"  , Amount = 1 , Unit =  Models.Enums.Unit.Teaspoon},
           new RecipeIngredient { Id = Guid.NewGuid(), RecipeId = new Guid("0c98d5db-4983-40cd-923a-df9135ed467e"), Name = "Pepper" , Amount = 1 ,Unit = Models.Enums.Unit.Teaspoon },
           new RecipeIngredient { Id = Guid.NewGuid(), RecipeId = new Guid("0c98d5db-4983-40cd-923a-df9135ed467e"), Name = "Garlic" , Amount = 2 , Unit = Models.Enums.Unit.Cup }
        );

            // Add the recipe instructions
            modelBuilder.Entity<RecipeInstruction>().HasData(
         new RecipeInstruction { Id = Guid.NewGuid(), RecipeId = new Guid("0c98d5db-4983-40cd-923a-df9135ed467e"), StepNumber = 1, StepDescription = "Preheat the grill to medium-high heat." },
         new RecipeInstruction { Id = Guid.NewGuid(), RecipeId = new Guid("0c98d5db-4983-40cd-923a-df9135ed467e"), StepNumber = 2, StepDescription = "Season the steak with salt and pepper." },
         new RecipeInstruction { Id = Guid.NewGuid(), RecipeId = new Guid("0c98d5db-4983-40cd-923a-df9135ed467e"), StepNumber = 3, StepDescription = "Grill the steak for 4-5 minutes per side for medium-rare." }
 // Add more instructions as needed
 );


            modelBuilder.Entity<Recipe>()
                 .HasMany(t => t.Tags)
                 .WithMany(r => r.Recipes)
                 .UsingEntity<Dictionary<string, object>>(
                     "RecipeTag",
                     j => j
                         .HasOne<Tag>()
                         .WithMany()
                         .HasForeignKey("TagId"),
                     j => j
                         .HasOne<Recipe>()
                         .WithMany()
                         .HasForeignKey("RecipeId"),
                     j =>
                     {
                         j.HasKey("TagId", "RecipeId");
                         j.HasData(
                             new { RecipeId = new Guid("0c98d5db-4983-40cd-923a-df9135ed467e"), TagId = new Guid("d4977b4a-8679-4350-95d4-baf729080eae") },
                             new { RecipeId = new Guid("0c98d5db-4983-40cd-923a-df9135ed467e") , TagId = new Guid("10386470-5c03-495d-8b43-51437069008e") }
                         );
                     }
                 );


        }

        



    }


}