using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RecipeSharingApi.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class RecipeSh10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cuisines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuisines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Policies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Policies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecommendationScore",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecommendationScore", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PolicyRole",
                columns: table => new
                {
                    PoliciesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RolesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicyRole", x => new { x.PoliciesId, x.RolesId });
                    table.ForeignKey(
                        name: "FK_PolicyRole_Policies_PoliciesId",
                        column: x => x.PoliciesId,
                        principalTable: "Policies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PolicyRole_Roles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PolicyRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PolicyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicyRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PolicyRoles_Policies_PolicyId",
                        column: x => x.PolicyId,
                        principalTable: "Policies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PolicyRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SaltedHashPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Collections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfRecipes = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Collections_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CookBook",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CookBook", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CookBook_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingListItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingListItem", x => new { x.Id, x.UserId, x.Name });
                    table.ForeignKey(
                        name: "FK_ShoppingListItem_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CuisineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrepTime = table.Column<int>(type: "int", nullable: false),
                    CookTime = table.Column<int>(type: "int", nullable: false),
                    Servings = table.Column<int>(type: "int", nullable: false),
                    Yield = table.Column<int>(type: "int", nullable: false),
                    Calories = table.Column<double>(type: "float", nullable: false),
                    AudioInstructions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VideoInstructions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CookBookId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recipes_CookBook_CookBookId",
                        column: x => x.CookBookId,
                        principalTable: "CookBook",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Recipes_Cuisines_CuisineId",
                        column: x => x.CuisineId,
                        principalTable: "Cuisines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recipes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CollectionRecipe",
                columns: table => new
                {
                    CollectionsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecipesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectionRecipe", x => new { x.CollectionsId, x.RecipesId });
                    table.ForeignKey(
                        name: "FK_CollectionRecipe_Collections_CollectionsId",
                        column: x => x.CollectionsId,
                        principalTable: "Collections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CollectionRecipe_Recipes_RecipesId",
                        column: x => x.RecipesId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InstructionSteps",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecipeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StepNumber = table.Column<int>(type: "int", nullable: false),
                    StepDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstructionSteps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstructionSteps_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipeIngredients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecipeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Unit = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeIngredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipeIngredients_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipeTag",
                columns: table => new
                {
                    RecipesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeTag", x => new { x.RecipesId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_RecipeTag_Recipes_RecipesId",
                        column: x => x.RecipesId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecipeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Policies",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0c19f0c5-3da9-435f-b784-81482e074738"), "UpdateReviews" },
                    { new Guid("0d423b1a-3bc6-48f3-bc9c-c1bb8d5a7071"), "GetATagById" },
                    { new Guid("0e8e465e-8abd-4c2f-a6a3-3796b0d702bc"), "PostCollections" },
                    { new Guid("0fce3dfd-befa-47f7-8a66-07964b6d8f4d"), "DeleteUser" },
                    { new Guid("1381225f-387b-49c9-b57a-6a1a379fdb8a"), "addRolePolicy" },
                    { new Guid("1b68dcb1-d256-4d9e-93f7-51008c268cb9"), "DeleteTag" },
                    { new Guid("1d77b3dd-fd7b-4db6-b528-a5c9470378dd"), "UpdateCuisine" },
                    { new Guid("1f1260ac-260e-4bc8-8cb3-a7fa5d8dc366"), "UpdateCookBook" },
                    { new Guid("257400bf-1dbf-4f30-83d7-d1f482651a8d"), "GetCookbookById" },
                    { new Guid("2844b8fb-a338-4801-b06b-6f80d923d6c2"), "UpdateRecipe" },
                    { new Guid("2b0088ca-6ee6-41a7-b931-ee61f786bd55"), "RemoveRecipeCollections" },
                    { new Guid("2bc75a12-6655-441c-af9d-32be58a5531e"), "GetNameOfUser" },
                    { new Guid("2c497d51-de9d-41a7-b23b-c4f51f6dd9d6"), "UpdateUserRole" },
                    { new Guid("384a15ef-8a8f-4152-8c3c-196749f9ab72"), "GetUserById" },
                    { new Guid("3b90b27c-5690-4305-a418-3f58efeade35"), "getUserCollections" },
                    { new Guid("3d041ace-b9ef-425f-b520-136e2dc8c5ba"), "MyData" },
                    { new Guid("3eb0b264-294e-4a1d-a748-5610d2a1f1f1"), "CreateATag" },
                    { new Guid("403015ab-9012-42d8-b13b-65366085303a"), "CreateCuisine" },
                    { new Guid("410595d2-43e0-4844-b672-a59a4151d7d5"), "getRoles" },
                    { new Guid("43d2a293-4093-42fd-b80e-c146f5890076"), "ChangePassword" },
                    { new Guid("58b74ad0-74c8-4909-b79b-60d31a57e49b"), "searchByEmail" },
                    { new Guid("5d5a00cb-4c9c-4950-aace-8dd956eaec0c"), "UpdateTag" },
                    { new Guid("65ab361d-916d-43cd-816f-49ed8688bf87"), "GetUserShopingList" },
                    { new Guid("68d3008d-ede9-4ac8-8eb9-0f8877358476"), "addPolicy" },
                    { new Guid("6929858c-31ad-40b0-b53d-bc0cc30548ab"), "AllCookbooks" },
                    { new Guid("6d966a93-0aa1-420b-bf37-d7288e8e638e"), "addrole" },
                    { new Guid("6f563976-863c-409d-8237-f08ed043ffcd"), "AllUserCookbooks" },
                    { new Guid("71c436da-3849-4459-8f26-7b299c74162f"), "AllCookbooksByPage" },
                    { new Guid("8460422a-5e83-4d24-82a2-eeb45b902406"), "AddRecipeOnCookbook" },
                    { new Guid("8884e0bf-6477-4efc-b5e2-a34fe1d1dda5"), "GetRecipeByUserId" },
                    { new Guid("8f69170f-064c-40b2-804e-c4b53bb3218d"), "GetLink" },
                    { new Guid("97fa4a01-82c8-4ea2-addc-cc5f4037dc1e"), "postRecipeInCollections" },
                    { new Guid("9b0a1cbc-bb03-48c7-89dd-24b622e6cb84"), "UpdateUser" },
                    { new Guid("a80c3363-4977-49f5-9327-1c9b76baa3ed"), "GetAllTags" },
                    { new Guid("a9401ffb-8d6a-47cd-b9ba-d5d740bb86f8"), "ReviewRecipe" },
                    { new Guid("ad057d33-b1ab-491f-a143-a7cd45399a7b"), "getCollectionsById" },
                    { new Guid("ae6e7487-2375-4e33-911c-74ca4d375f5a"), "putCollections" },
                    { new Guid("bf9f8380-023f-4660-9c0f-f8fce5295f48"), "DeleteCookbook" },
                    { new Guid("c3001055-9124-4aea-a1ac-2dccb4257512"), "GetItemFromShopingListById" },
                    { new Guid("ccb9a20c-6dc2-4c37-a792-33790c42e256"), "CreateRecipe" },
                    { new Guid("cefaeb1f-7ca5-41dd-bd1c-e0b66f5f6051"), "PostCookbooks" },
                    { new Guid("d0f9bb82-9abe-4841-ab56-dd79cff023d3"), "getCollections" },
                    { new Guid("d2194a88-8cb5-41c3-b40d-227ea502c1a8"), "AllUsers" },
                    { new Guid("d5662e2c-f6db-4601-8911-a3faf99d03d2"), "CreateShopingList" },
                    { new Guid("d6a80b7d-70f3-4a63-a1a4-7ff70c110a49"), "GetAllUserRecipe" },
                    { new Guid("d787eb55-0b45-45c6-a6fb-0566209831b0"), "DeleteRecipeFromCookbook" },
                    { new Guid("e00b12ee-cde2-44e3-ba10-ef4b45390252"), "getPolicies" },
                    { new Guid("e73cf4bb-4a96-49c2-a3ef-428915e2f968"), "DeleteRecipe" },
                    { new Guid("e8fbc57b-4837-4d73-be89-e9f6ce4dc328"), "DelteRewiews" },
                    { new Guid("ea13f8d7-a3ed-4464-bfbb-f4af8db2b567"), "DeleteItemFromShopingList" },
                    { new Guid("ec67c7c7-653d-4078-b9ec-82f9d188fd69"), "DeleteCuisine" },
                    { new Guid("f7d9d48f-62c4-44eb-9ac8-1e7ce066c88d"), "deleteCollections" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("24bade73-46d0-40dd-95e7-42352504fe2d"), "user" },
                    { new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd"), "admin" },
                    { new Guid("ed263967-56fd-451c-a997-952d26cf7aff"), "editor" }
                });

            migrationBuilder.InsertData(
                table: "PolicyRoles",
                columns: new[] { "Id", "PolicyId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("010d9c89-fc25-4a03-adfb-15c7b433410d"), new Guid("1b68dcb1-d256-4d9e-93f7-51008c268cb9"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("01457afe-d826-4bd5-88b3-6e9d63b7678c"), new Guid("ad057d33-b1ab-491f-a143-a7cd45399a7b"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("04e578ae-8046-41d4-afaa-bc2a9da1600e"), new Guid("0e8e465e-8abd-4c2f-a6a3-3796b0d702bc"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("063282fd-1a93-49f8-b956-1527799ec029"), new Guid("a9401ffb-8d6a-47cd-b9ba-d5d740bb86f8"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("06e921d3-df39-4aa0-a241-e8c1785a5fe5"), new Guid("d787eb55-0b45-45c6-a6fb-0566209831b0"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("08386fa8-8491-44b2-a6d3-8b25ebf365f5"), new Guid("3eb0b264-294e-4a1d-a748-5610d2a1f1f1"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("0fb58aa4-d10e-428d-9d80-8eca07675764"), new Guid("d2194a88-8cb5-41c3-b40d-227ea502c1a8"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("102f3a03-8753-427b-8623-4cdb4f090a2f"), new Guid("2c497d51-de9d-41a7-b23b-c4f51f6dd9d6"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("10fbd1d8-88df-48af-b956-fb6c66071d3a"), new Guid("3d041ace-b9ef-425f-b520-136e2dc8c5ba"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("16c5fda3-d490-47c0-bb26-7a0281ed0d88"), new Guid("a80c3363-4977-49f5-9327-1c9b76baa3ed"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("19c0cdd3-3964-4ded-a0c9-acf811938908"), new Guid("d2194a88-8cb5-41c3-b40d-227ea502c1a8"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("1da61afd-fdab-485c-8b72-ba21b9dc3f1c"), new Guid("9b0a1cbc-bb03-48c7-89dd-24b622e6cb84"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("1dee0a57-1fde-4b9d-ae4e-8467eb6fcd50"), new Guid("5d5a00cb-4c9c-4950-aace-8dd956eaec0c"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("1ee64810-7333-43ae-a318-c6f9374cabc5"), new Guid("c3001055-9124-4aea-a1ac-2dccb4257512"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("28533d8b-365b-455b-b0bb-654852f40a55"), new Guid("2b0088ca-6ee6-41a7-b931-ee61f786bd55"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("293f0f89-dd04-4fbb-b4b8-90a717211b9d"), new Guid("d5662e2c-f6db-4601-8911-a3faf99d03d2"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("2c5af8fa-b4d7-4c46-a271-517024f4edef"), new Guid("257400bf-1dbf-4f30-83d7-d1f482651a8d"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("2c9171d6-ba11-47ad-b2ce-c37ac04e9703"), new Guid("3d041ace-b9ef-425f-b520-136e2dc8c5ba"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("2fffa6b8-3cae-430d-ac77-c18aed59dff0"), new Guid("9b0a1cbc-bb03-48c7-89dd-24b622e6cb84"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("375cda87-83be-4427-a255-ff810e1735bc"), new Guid("cefaeb1f-7ca5-41dd-bd1c-e0b66f5f6051"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("3ae26d69-270f-4d91-93a7-9818f905ce9f"), new Guid("71c436da-3849-4459-8f26-7b299c74162f"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("42569bf5-37d2-466b-bb2c-2347fceae1fe"), new Guid("3eb0b264-294e-4a1d-a748-5610d2a1f1f1"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("44112be0-f6ee-41e7-b8ee-be15bbd78d32"), new Guid("8460422a-5e83-4d24-82a2-eeb45b902406"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("4bdb6db9-9476-4b7e-9d06-66df061404c1"), new Guid("384a15ef-8a8f-4152-8c3c-196749f9ab72"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("4ec0f02c-8236-48ae-b98a-8a7ed17a23e5"), new Guid("3b90b27c-5690-4305-a418-3f58efeade35"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("53107267-7db6-4371-8a39-b8d500d5a03e"), new Guid("d0f9bb82-9abe-4841-ab56-dd79cff023d3"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("58e6dc43-4cd2-4034-96d3-a083edc3bd0a"), new Guid("e73cf4bb-4a96-49c2-a3ef-428915e2f968"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("5d29dfec-ae72-4d3b-aafb-3cc529741ce3"), new Guid("ccb9a20c-6dc2-4c37-a792-33790c42e256"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("5d64bfd8-d37a-4532-afa8-c48c86e7849b"), new Guid("6f563976-863c-409d-8237-f08ed043ffcd"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("601b8e73-de27-48b2-b226-1c7a03615c63"), new Guid("d0f9bb82-9abe-4841-ab56-dd79cff023d3"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("640353e0-5bcc-459f-8b66-7d36202fe7ff"), new Guid("bf9f8380-023f-4660-9c0f-f8fce5295f48"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("69156c00-237a-4519-ae4d-c961e14990f8"), new Guid("3eb0b264-294e-4a1d-a748-5610d2a1f1f1"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("69a0df84-f8b5-4638-b0bc-aea14572c1a4"), new Guid("65ab361d-916d-43cd-816f-49ed8688bf87"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("6a70449d-7bd9-482c-9ed2-f20f66b4b460"), new Guid("71c436da-3849-4459-8f26-7b299c74162f"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("6a89a49a-8930-428b-a75e-674215d0b7d7"), new Guid("ec67c7c7-653d-4078-b9ec-82f9d188fd69"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("6e604266-f58b-4da7-ac1e-fa4558349a2d"), new Guid("71c436da-3849-4459-8f26-7b299c74162f"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("7102bdb7-7642-466a-b3c2-d7a2b229e328"), new Guid("410595d2-43e0-4844-b672-a59a4151d7d5"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("74204d5d-05ff-4052-b581-1634ceaab895"), new Guid("9b0a1cbc-bb03-48c7-89dd-24b622e6cb84"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("74cc82d3-f5d1-4224-88fa-4d75e83db327"), new Guid("d5662e2c-f6db-4601-8911-a3faf99d03d2"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("7a07fde4-0d6c-4dd4-87d9-b099fa3d446c"), new Guid("e73cf4bb-4a96-49c2-a3ef-428915e2f968"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("7fca53a4-757d-4b00-b423-7bf6cb6a85e4"), new Guid("0fce3dfd-befa-47f7-8a66-07964b6d8f4d"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("7fef7347-f240-46e2-be28-336223b030af"), new Guid("f7d9d48f-62c4-44eb-9ac8-1e7ce066c88d"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("8119d26a-ed79-4543-b997-e51529b34ee7"), new Guid("ccb9a20c-6dc2-4c37-a792-33790c42e256"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("82207918-f621-4b24-998c-faeb3edbc768"), new Guid("1b68dcb1-d256-4d9e-93f7-51008c268cb9"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("8254ad4e-9bda-46f8-96d9-89f591992f5f"), new Guid("58b74ad0-74c8-4909-b79b-60d31a57e49b"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("845fb6e1-024b-4e78-bc83-5c050268c2fc"), new Guid("2844b8fb-a338-4801-b06b-6f80d923d6c2"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("8577e681-2c76-4fa0-aa2a-96afa49bf034"), new Guid("1f1260ac-260e-4bc8-8cb3-a7fa5d8dc366"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("87a547d2-6c24-4327-a31a-f87fec52678e"), new Guid("0fce3dfd-befa-47f7-8a66-07964b6d8f4d"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("8cd2bb4d-35f9-4670-abc9-b3c7177177c3"), new Guid("ea13f8d7-a3ed-4464-bfbb-f4af8db2b567"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("8d44139c-81fc-4764-90e1-2eaab2e4dad0"), new Guid("6929858c-31ad-40b0-b53d-bc0cc30548ab"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("8f437899-beff-4a1a-87f3-8cd507ea76a8"), new Guid("a9401ffb-8d6a-47cd-b9ba-d5d740bb86f8"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("9024f973-560a-478c-b188-1e73afe8424f"), new Guid("1d77b3dd-fd7b-4db6-b528-a5c9470378dd"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("95d1229e-7b2a-48a0-84e5-7e1cd2c7b729"), new Guid("3d041ace-b9ef-425f-b520-136e2dc8c5ba"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("96e505f7-3f75-458b-a6db-5b93b6ecd05a"), new Guid("0c19f0c5-3da9-435f-b784-81482e074738"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("9bae4fad-8125-47b4-a3a6-e10199ac1d23"), new Guid("ea13f8d7-a3ed-4464-bfbb-f4af8db2b567"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("9f14248c-d1eb-4b59-b460-c4f81e15400f"), new Guid("384a15ef-8a8f-4152-8c3c-196749f9ab72"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("a011e3a5-06b9-4fba-a4b9-9a9bc74a0138"), new Guid("d5662e2c-f6db-4601-8911-a3faf99d03d2"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("a499bd30-8515-4130-be15-0cbc1463da94"), new Guid("0c19f0c5-3da9-435f-b784-81482e074738"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("a78ce896-1244-449d-9f09-11a226c6a76d"), new Guid("ae6e7487-2375-4e33-911c-74ca4d375f5a"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("aaac76f6-db4a-4b8b-b7f0-f1cf00073509"), new Guid("43d2a293-4093-42fd-b80e-c146f5890076"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("b43e07fd-f23a-4063-90f6-4ced65e7a879"), new Guid("c3001055-9124-4aea-a1ac-2dccb4257512"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("b523d61e-c252-4a6d-aa9a-9861d76251d4"), new Guid("d787eb55-0b45-45c6-a6fb-0566209831b0"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("b8a80d95-14c1-4f3d-95f0-e473f5c60cd4"), new Guid("1381225f-387b-49c9-b57a-6a1a379fdb8a"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("bb014183-164a-4360-8e43-47c19c43a6f7"), new Guid("e8fbc57b-4837-4d73-be89-e9f6ce4dc328"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("bb43096e-6e43-4976-a74b-e0d3873ea577"), new Guid("6f563976-863c-409d-8237-f08ed043ffcd"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("bbde4f98-c619-4cfe-9ec7-231ed0164ae7"), new Guid("43d2a293-4093-42fd-b80e-c146f5890076"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("befc38cf-b2f4-4212-b5fe-38b28df1647f"), new Guid("c3001055-9124-4aea-a1ac-2dccb4257512"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("bf3fba91-bc29-40a9-a7a0-949f2d71b01b"), new Guid("0d423b1a-3bc6-48f3-bc9c-c1bb8d5a7071"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("c2c4746d-ed7e-4843-9fbb-eb1dda2b755e"), new Guid("d6a80b7d-70f3-4a63-a1a4-7ff70c110a49"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("c330f51e-88b5-4624-bf98-0ea1ba7ec77e"), new Guid("1f1260ac-260e-4bc8-8cb3-a7fa5d8dc366"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("c3c23837-1e6d-44ec-bab6-744e081b6411"), new Guid("a80c3363-4977-49f5-9327-1c9b76baa3ed"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("c52a71c3-3d2f-4b2d-aef2-869b7ca3352c"), new Guid("e8fbc57b-4837-4d73-be89-e9f6ce4dc328"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("c58b4ff4-a015-4014-9977-98521b269259"), new Guid("97fa4a01-82c8-4ea2-addc-cc5f4037dc1e"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("c8b8ce4c-f183-4ddd-9861-797d7348835c"), new Guid("a80c3363-4977-49f5-9327-1c9b76baa3ed"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("cd3d7298-ea3f-486e-bc7d-67449549f971"), new Guid("6929858c-31ad-40b0-b53d-bc0cc30548ab"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("cf944e2d-44e9-49e2-ad70-d91bf375f0dc"), new Guid("2bc75a12-6655-441c-af9d-32be58a5531e"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("d4863607-64c1-4ddb-8836-fd125c5535a2"), new Guid("ea13f8d7-a3ed-4464-bfbb-f4af8db2b567"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("d51a914f-905c-43e9-a6f0-d20e2e45d0de"), new Guid("8460422a-5e83-4d24-82a2-eeb45b902406"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("d6c9da4b-9666-4885-b236-9701e29429c1"), new Guid("6d966a93-0aa1-420b-bf37-d7288e8e638e"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("de661027-a9f1-4e7d-8409-82bd9924bcb4"), new Guid("0d423b1a-3bc6-48f3-bc9c-c1bb8d5a7071"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("df443c46-1bd7-435b-b915-1e04f3f941c6"), new Guid("403015ab-9012-42d8-b13b-65366085303a"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("e00e2252-367e-473c-b4a7-f7b3a91a6def"), new Guid("8f69170f-064c-40b2-804e-c4b53bb3218d"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("e40c28ea-32fa-4c91-9c11-6118a2383081"), new Guid("d6a80b7d-70f3-4a63-a1a4-7ff70c110a49"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("e413b4ab-9cec-44b4-9a50-622292ecb581"), new Guid("8884e0bf-6477-4efc-b5e2-a34fe1d1dda5"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("e45be0ff-5ce3-4989-a3c6-12d5c46f9f59"), new Guid("0d423b1a-3bc6-48f3-bc9c-c1bb8d5a7071"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("e5be2eea-b888-4a30-acc1-ad890b8ffd75"), new Guid("5d5a00cb-4c9c-4950-aace-8dd956eaec0c"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("e8cb52da-99ac-4761-bc0e-2f46bd457278"), new Guid("cefaeb1f-7ca5-41dd-bd1c-e0b66f5f6051"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("eae81830-f78d-441d-9fd5-9f28eef10b58"), new Guid("d2194a88-8cb5-41c3-b40d-227ea502c1a8"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("eb9cf856-31ab-47af-a4e3-eb1fde0d34aa"), new Guid("2844b8fb-a338-4801-b06b-6f80d923d6c2"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("ee625111-da9e-40fa-98cf-24639488621f"), new Guid("257400bf-1dbf-4f30-83d7-d1f482651a8d"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("f02106e3-57ac-4029-83c0-1a9b8eedb13d"), new Guid("43d2a293-4093-42fd-b80e-c146f5890076"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("f11b64b8-5d86-47b2-a00f-a21243d3fe1c"), new Guid("bf9f8380-023f-4660-9c0f-f8fce5295f48"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("f50ac3c4-5261-4850-a393-ab0f2f3f113d"), new Guid("2c497d51-de9d-41a7-b23b-c4f51f6dd9d6"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("fabc897b-11c2-40bc-9800-a9148b2a05a4"), new Guid("0fce3dfd-befa-47f7-8a66-07964b6d8f4d"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("fb2752b6-dbdc-4103-9378-ac0cf2185a9d"), new Guid("68d3008d-ede9-4ac8-8eb9-0f8877358476"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("fea28b08-c019-472b-91e7-fb273a955875"), new Guid("5d5a00cb-4c9c-4950-aace-8dd956eaec0c"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("ff222385-81e3-4dfd-b3f4-6df2f77f8290"), new Guid("e00b12ee-cde2-44e3-ba10-ef4b45390252"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("ff636c4f-9f1d-44ce-8b8e-ac253b8f8a85"), new Guid("e8fbc57b-4837-4d73-be89-e9f6ce4dc328"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollectionRecipe_RecipesId",
                table: "CollectionRecipe",
                column: "RecipesId");

            migrationBuilder.CreateIndex(
                name: "IX_Collections_UserId",
                table: "Collections",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CookBook_UserId",
                table: "CookBook",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_InstructionSteps_RecipeId",
                table: "InstructionSteps",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyRole_RolesId",
                table: "PolicyRole",
                column: "RolesId");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyRoles_PolicyId",
                table: "PolicyRoles",
                column: "PolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyRoles_RoleId",
                table: "PolicyRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredients_RecipeId",
                table: "RecipeIngredients",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_CookBookId",
                table: "Recipes",
                column: "CookBookId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_CuisineId",
                table: "Recipes",
                column: "CuisineId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_UserId",
                table: "Recipes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeTag_TagsId",
                table: "RecipeTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_RecipeId",
                table: "Reviews",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingListItem_UserId",
                table: "ShoppingListItem",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_Name",
                table: "Tags",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollectionRecipe");

            migrationBuilder.DropTable(
                name: "InstructionSteps");

            migrationBuilder.DropTable(
                name: "PolicyRole");

            migrationBuilder.DropTable(
                name: "PolicyRoles");

            migrationBuilder.DropTable(
                name: "RecipeIngredients");

            migrationBuilder.DropTable(
                name: "RecipeTag");

            migrationBuilder.DropTable(
                name: "RecommendationScore");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "ShoppingListItem");

            migrationBuilder.DropTable(
                name: "Collections");

            migrationBuilder.DropTable(
                name: "Policies");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "CookBook");

            migrationBuilder.DropTable(
                name: "Cuisines");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
