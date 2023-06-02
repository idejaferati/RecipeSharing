using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RecipeSharingApi.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class RecipeSharing10 : Migration
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
                    TagId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecipeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeTag", x => new { x.TagId, x.RecipeId });
                    table.ForeignKey(
                        name: "FK_RecipeTag_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeTag_Tags_TagId",
                        column: x => x.TagId,
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
                table: "Cuisines",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("ca79e18d-2f4a-4d3e-8c7d-5b516b9eb516"), "Germany" },
                    { new Guid("d89543a3-0a4a-4830-8092-63c77b049a8f"), "USA" },
                    { new Guid("f3114be0-104b-463e-9f40-66f5450b0eb1"), "United Kingdom" }
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
                table: "Tags",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("10386470-5c03-495d-8b43-51437069008e"), "#perfect" },
                    { new Guid("55fdbfbc-2f46-4b10-83b2-0d4dd6aa54c3"), "#IceCream" },
                    { new Guid("d4977b4a-8679-4350-95d4-baf729080eae"), "#delicious" }
                });

            migrationBuilder.InsertData(
                table: "PolicyRoles",
                columns: new[] { "Id", "PolicyId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("015d1a1d-3f00-4fc5-91cd-60633e8a5177"), new Guid("e73cf4bb-4a96-49c2-a3ef-428915e2f968"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("089a8cbc-c22c-450e-a33b-af3f25f672a7"), new Guid("a80c3363-4977-49f5-9327-1c9b76baa3ed"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("0d24eeec-dd01-4872-a009-9b9a806ec2f3"), new Guid("d5662e2c-f6db-4601-8911-a3faf99d03d2"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("0f0562cf-3d42-4c80-9136-0259fde580d1"), new Guid("d6a80b7d-70f3-4a63-a1a4-7ff70c110a49"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("1395cbbd-8eb5-4556-8bd0-8f3540714320"), new Guid("e8fbc57b-4837-4d73-be89-e9f6ce4dc328"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("1892c4aa-2a26-4a32-a42f-7f717714f671"), new Guid("cefaeb1f-7ca5-41dd-bd1c-e0b66f5f6051"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("19516f17-954e-4d9e-b08d-03e776895c4d"), new Guid("f7d9d48f-62c4-44eb-9ac8-1e7ce066c88d"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("1c5d054e-7805-4822-ba66-3e3206c0d26b"), new Guid("d6a80b7d-70f3-4a63-a1a4-7ff70c110a49"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("20747f7f-fbf2-47c2-9b90-7b4ae39887c0"), new Guid("257400bf-1dbf-4f30-83d7-d1f482651a8d"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("23507507-2549-4ed8-8d26-ecc07e63e1a9"), new Guid("68d3008d-ede9-4ac8-8eb9-0f8877358476"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("25a6dd2e-5e59-47ba-9c4a-4ec169d1d372"), new Guid("a9401ffb-8d6a-47cd-b9ba-d5d740bb86f8"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("27111168-47b6-4137-8534-d04dc54c84bc"), new Guid("d2194a88-8cb5-41c3-b40d-227ea502c1a8"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("2def1ae7-d407-4ee7-abde-cc1f6813cea4"), new Guid("8884e0bf-6477-4efc-b5e2-a34fe1d1dda5"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("32e82eaa-7c8c-4b61-ac61-85f65e55e9c4"), new Guid("71c436da-3849-4459-8f26-7b299c74162f"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("35082bc6-e886-452d-b80d-92c5df6c071f"), new Guid("410595d2-43e0-4844-b672-a59a4151d7d5"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("36727459-c965-4d01-8dbe-6264ec5c1a26"), new Guid("2c497d51-de9d-41a7-b23b-c4f51f6dd9d6"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("396f4c7d-eccd-4e8f-8229-7d0e3725310a"), new Guid("2844b8fb-a338-4801-b06b-6f80d923d6c2"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("3a52355c-7e48-4d4c-896a-25a8545b49fa"), new Guid("e8fbc57b-4837-4d73-be89-e9f6ce4dc328"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("3cded58b-7325-4951-8878-22aa1c5e0859"), new Guid("d2194a88-8cb5-41c3-b40d-227ea502c1a8"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("404aa827-60cc-4613-91f7-39e30fd571db"), new Guid("c3001055-9124-4aea-a1ac-2dccb4257512"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("40cef3d8-9fb8-4d25-8be7-1629bbd7f022"), new Guid("0d423b1a-3bc6-48f3-bc9c-c1bb8d5a7071"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("41a3178b-86de-4801-ae84-e4760480251b"), new Guid("58b74ad0-74c8-4909-b79b-60d31a57e49b"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("4302ac9f-1343-426f-ad4c-ed2bd5cac497"), new Guid("403015ab-9012-42d8-b13b-65366085303a"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("43274c82-fa9a-4499-93b2-9080f4a67d08"), new Guid("71c436da-3849-4459-8f26-7b299c74162f"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("46af5f38-32a3-434c-8d61-c094b0905907"), new Guid("ad057d33-b1ab-491f-a143-a7cd45399a7b"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("46f9cb53-775a-4183-a4ad-4c0b8131c444"), new Guid("ea13f8d7-a3ed-4464-bfbb-f4af8db2b567"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("489629d4-edef-4fc5-a4f6-572000ce3c6a"), new Guid("d2194a88-8cb5-41c3-b40d-227ea502c1a8"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("4ae6c65b-3ad4-40d6-81fc-adb738a1ffdb"), new Guid("c3001055-9124-4aea-a1ac-2dccb4257512"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("4b5f88c6-edac-4d32-a71f-22bfd33c59a9"), new Guid("9b0a1cbc-bb03-48c7-89dd-24b622e6cb84"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("4c92b5ce-2486-4568-af10-3908e84e58c4"), new Guid("71c436da-3849-4459-8f26-7b299c74162f"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("4d392ee7-0c40-4657-9b11-fc7f318e1143"), new Guid("6d966a93-0aa1-420b-bf37-d7288e8e638e"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("4d3c89d5-5e1c-4a0d-b056-b15033fa18a3"), new Guid("2b0088ca-6ee6-41a7-b931-ee61f786bd55"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("4e97e177-19a3-47b6-a59c-cdbb185c4664"), new Guid("6f563976-863c-409d-8237-f08ed043ffcd"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("4f397533-ad61-424c-8fa7-732a86a08dbf"), new Guid("3eb0b264-294e-4a1d-a748-5610d2a1f1f1"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("5097d6c4-717a-44ca-9d75-42f19a2e34ac"), new Guid("2bc75a12-6655-441c-af9d-32be58a5531e"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("52e1909f-8222-4c3d-8531-318c6174a826"), new Guid("ec67c7c7-653d-4078-b9ec-82f9d188fd69"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("55c9c620-4c9a-4c73-8a59-2b8a70e4d0ee"), new Guid("1b68dcb1-d256-4d9e-93f7-51008c268cb9"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("59ccd9ce-a09c-4933-b9f2-cfa3967f9236"), new Guid("0fce3dfd-befa-47f7-8a66-07964b6d8f4d"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("5adeb5f2-c0bc-451e-b1b0-8889d49b4b3d"), new Guid("bf9f8380-023f-4660-9c0f-f8fce5295f48"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("5d502594-4865-4235-9d57-3c78f24a2ae4"), new Guid("0d423b1a-3bc6-48f3-bc9c-c1bb8d5a7071"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("5f80ce0e-2a06-4091-8da2-2602c1796303"), new Guid("2844b8fb-a338-4801-b06b-6f80d923d6c2"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("641def55-2609-4f2f-9e19-d0a441668e9a"), new Guid("e73cf4bb-4a96-49c2-a3ef-428915e2f968"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("646d7169-f51a-4dcd-9ebb-3dd06c34b5d5"), new Guid("cefaeb1f-7ca5-41dd-bd1c-e0b66f5f6051"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("6903cbde-71c5-4ebf-94aa-6234d43144ea"), new Guid("384a15ef-8a8f-4152-8c3c-196749f9ab72"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("70f20b08-8124-4b0b-b98c-fe2cb4e9cea8"), new Guid("9b0a1cbc-bb03-48c7-89dd-24b622e6cb84"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("74adb13e-e98d-41df-9a3d-c60518252c4e"), new Guid("8460422a-5e83-4d24-82a2-eeb45b902406"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("74d19545-3632-4701-99c2-a9f76069a323"), new Guid("0fce3dfd-befa-47f7-8a66-07964b6d8f4d"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("763f6e06-3381-47ac-b00a-6deb5185932d"), new Guid("6f563976-863c-409d-8237-f08ed043ffcd"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("7674ca8f-2102-4993-b476-55a02a2d4411"), new Guid("1f1260ac-260e-4bc8-8cb3-a7fa5d8dc366"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("7aa9a322-7ff3-43c6-8035-c5b129291ee0"), new Guid("0fce3dfd-befa-47f7-8a66-07964b6d8f4d"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("7cafd8a7-35a0-47b2-b3db-f7ac64aa381a"), new Guid("3d041ace-b9ef-425f-b520-136e2dc8c5ba"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("8066f499-5b98-48ff-9f53-5ce93e12748a"), new Guid("d0f9bb82-9abe-4841-ab56-dd79cff023d3"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("80dd8f02-e140-41bf-9c52-2fbd56395fc7"), new Guid("3d041ace-b9ef-425f-b520-136e2dc8c5ba"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("83880aa7-5a4a-434f-b376-549d61941826"), new Guid("ccb9a20c-6dc2-4c37-a792-33790c42e256"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("85865b78-e58a-47a4-b9f7-3233067de4c6"), new Guid("1381225f-387b-49c9-b57a-6a1a379fdb8a"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("8c62934b-6d0a-47f7-93ce-41e4a5440c0c"), new Guid("e00b12ee-cde2-44e3-ba10-ef4b45390252"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("8d4e0e49-5011-4068-baab-d4a4df638ec7"), new Guid("c3001055-9124-4aea-a1ac-2dccb4257512"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("8e2aeab3-6ac6-4e90-8a14-b50842fa9022"), new Guid("d0f9bb82-9abe-4841-ab56-dd79cff023d3"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("94a6b89e-abce-4d78-aa1b-9dd9ad5e8d49"), new Guid("0c19f0c5-3da9-435f-b784-81482e074738"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("94fae39b-6b33-4483-8861-790fc52cc343"), new Guid("65ab361d-916d-43cd-816f-49ed8688bf87"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("95327c77-1a98-487c-ab30-28b991f7ac75"), new Guid("1d77b3dd-fd7b-4db6-b528-a5c9470378dd"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("9d92dc23-72d2-4ad8-aba9-3b391f2d736d"), new Guid("d5662e2c-f6db-4601-8911-a3faf99d03d2"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("9d9f6b7a-e453-4a14-b2ee-714ed33f4cd6"), new Guid("43d2a293-4093-42fd-b80e-c146f5890076"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("9dfb3730-6d1c-438f-829f-2c4b68d5b073"), new Guid("2c497d51-de9d-41a7-b23b-c4f51f6dd9d6"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("9e1f9a08-5349-4328-a7b9-21bfcb5b6547"), new Guid("3eb0b264-294e-4a1d-a748-5610d2a1f1f1"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("9e6a383c-f87d-482b-a774-82d9159706e9"), new Guid("6929858c-31ad-40b0-b53d-bc0cc30548ab"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("a050655c-3e57-4add-a120-f8b39c7af97c"), new Guid("0e8e465e-8abd-4c2f-a6a3-3796b0d702bc"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("a3b65b6c-29e9-4202-b476-0881e7f49c0f"), new Guid("97fa4a01-82c8-4ea2-addc-cc5f4037dc1e"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("a4dfc0a3-9098-458c-9ae8-479753cdcd39"), new Guid("a80c3363-4977-49f5-9327-1c9b76baa3ed"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("a508bccb-8876-43dc-a489-70b4f7dc0797"), new Guid("ea13f8d7-a3ed-4464-bfbb-f4af8db2b567"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("a5567d25-3f20-41f6-a379-ca382f5b93c3"), new Guid("ccb9a20c-6dc2-4c37-a792-33790c42e256"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("a58de499-a5a7-4ea4-a909-4c5bda2553cc"), new Guid("d787eb55-0b45-45c6-a6fb-0566209831b0"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("b1e9ddae-9ffd-492a-85d0-81cd3f696caf"), new Guid("43d2a293-4093-42fd-b80e-c146f5890076"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("b64aebcb-26e6-48b5-a29b-f69fbf29e565"), new Guid("257400bf-1dbf-4f30-83d7-d1f482651a8d"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("b688e9a3-ddf4-4c13-91fd-d7ab9614e9c2"), new Guid("5d5a00cb-4c9c-4950-aace-8dd956eaec0c"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("bc943ae0-bbe2-406e-a64b-4dcdbde0be97"), new Guid("5d5a00cb-4c9c-4950-aace-8dd956eaec0c"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("bde1d113-504f-41dc-8809-577d1e5e669d"), new Guid("9b0a1cbc-bb03-48c7-89dd-24b622e6cb84"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("bfca08b9-45cb-400c-b1c3-277fca70938d"), new Guid("e8fbc57b-4837-4d73-be89-e9f6ce4dc328"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("c29f89f2-07a3-4392-96cd-c355770b16c6"), new Guid("6929858c-31ad-40b0-b53d-bc0cc30548ab"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("c504d119-37d9-4f36-aaa7-6a4c2e986fa6"), new Guid("a9401ffb-8d6a-47cd-b9ba-d5d740bb86f8"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("c725735b-4cc7-4aa3-936b-508885a24786"), new Guid("bf9f8380-023f-4660-9c0f-f8fce5295f48"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("ceee6807-fda3-4696-b11b-3449b3be90ed"), new Guid("1b68dcb1-d256-4d9e-93f7-51008c268cb9"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("cfeb249f-ef3a-47bb-a6c0-f2b11d50f7f3"), new Guid("ae6e7487-2375-4e33-911c-74ca4d375f5a"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("d23a9194-7ba0-445b-9b89-0f0e85c51a5f"), new Guid("1f1260ac-260e-4bc8-8cb3-a7fa5d8dc366"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("d479b70b-a078-4369-8050-5c53aaf52436"), new Guid("43d2a293-4093-42fd-b80e-c146f5890076"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("d7ceadb0-d827-4346-97c5-b4e4b49d7176"), new Guid("8460422a-5e83-4d24-82a2-eeb45b902406"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("d9a8b57e-9ac7-4d10-ad1d-cd8d4de29d11"), new Guid("5d5a00cb-4c9c-4950-aace-8dd956eaec0c"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("da136464-942d-40e2-80d9-57d0b4cf6999"), new Guid("3eb0b264-294e-4a1d-a748-5610d2a1f1f1"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("df02f766-da61-450e-8d70-c52b03fcc411"), new Guid("3b90b27c-5690-4305-a418-3f58efeade35"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("e3629207-d544-4e78-9789-5412de361523"), new Guid("d5662e2c-f6db-4601-8911-a3faf99d03d2"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("e3c0cf8a-9047-4e8d-9af5-7ffac6a9722a"), new Guid("0c19f0c5-3da9-435f-b784-81482e074738"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("f06a0539-8d88-4b75-929f-586d4351239a"), new Guid("0d423b1a-3bc6-48f3-bc9c-c1bb8d5a7071"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("f31893ea-142b-442c-a945-2ecb15d47a8c"), new Guid("3d041ace-b9ef-425f-b520-136e2dc8c5ba"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("f34cd34e-3a75-4670-8d41-45b1b6409d2c"), new Guid("a80c3363-4977-49f5-9327-1c9b76baa3ed"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("f745fafd-5d53-4eeb-b4cd-9552828627eb"), new Guid("384a15ef-8a8f-4152-8c3c-196749f9ab72"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("fccb120e-8b69-419e-b8af-73881c35af71"), new Guid("8f69170f-064c-40b2-804e-c4b53bb3218d"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("fcd17049-e50f-42ca-8f03-69fee39ec046"), new Guid("ea13f8d7-a3ed-4464-bfbb-f4af8db2b567"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("fea02337-52ab-4add-ac2b-da346c660da5"), new Guid("d787eb55-0b45-45c6-a6fb-0566209831b0"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "Gender", "LastName", "PhoneNumber", "RoleId", "Salt", "SaltedHashPassword" },
                values: new object[,]
                {
                    { new Guid("1f19e7bd-c9ff-4fd6-8a1d-5e242c5794c4"), "admin@gmail.com", "Admin", "Male", "User", "123456789", new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd"), "$2a$11$dOev5hXKaHxBdLbLrjmgUO", "$2a$11$dOev5hXKaHxBdLbLrjmgUOX.yeQJdcw2KnBqYjBCIMRaKScfNqvnq" },
                    { new Guid("3e80e914-e301-4cb2-9290-206dcefe364c"), "user@example.com", "Regular", "Female", "User", "987654321", new Guid("24bade73-46d0-40dd-95e7-42352504fe2d"), "$2a$11$mISI/pB5Z0LkjBdG3DbZl.", "$2a$11$mISI/pB5Z0LkjBdG3DbZl.WGOh5B86pbsEW6tWHdS6OKZscYgt8X." }
                });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "AudioInstructions", "Calories", "CookBookId", "CookTime", "CuisineId", "Description", "Name", "PrepTime", "Servings", "UserId", "VideoInstructions", "Yield" },
                values: new object[] { new Guid("0c98d5db-4983-40cd-923a-df9135ed467e"), "https://iamafoodblog.b-cdn.net/wp-content/uploads/2021/02/how-to-cook-steak-1061w.jpg", 500.0, null, 20, new Guid("d89543a3-0a4a-4830-8092-63c77b049a8f"), "Delicious steak recipe", "Steak Recipe", 15, 2, new Guid("3e80e914-e301-4cb2-9290-206dcefe364c"), "Not available", 2 });

            migrationBuilder.InsertData(
                table: "InstructionSteps",
                columns: new[] { "Id", "RecipeId", "StepDescription", "StepNumber" },
                values: new object[,]
                {
                    { new Guid("182dc3e3-6074-4472-a019-ab66255de920"), new Guid("0c98d5db-4983-40cd-923a-df9135ed467e"), "Preheat the grill to medium-high heat.", 1 },
                    { new Guid("7982404a-3fe5-4c18-a49c-939f1858c8f5"), new Guid("0c98d5db-4983-40cd-923a-df9135ed467e"), "Grill the steak for 4-5 minutes per side for medium-rare.", 3 },
                    { new Guid("d1884375-0cb1-4b3c-a0b8-d382518a6137"), new Guid("0c98d5db-4983-40cd-923a-df9135ed467e"), "Season the steak with salt and pepper.", 2 }
                });

            migrationBuilder.InsertData(
                table: "RecipeIngredients",
                columns: new[] { "Id", "Amount", "Name", "RecipeId", "Unit" },
                values: new object[,]
                {
                    { new Guid("16b42de4-b58f-4746-9321-61f8fe12307e"), 1.0, "Salt", new Guid("0c98d5db-4983-40cd-923a-df9135ed467e"), 0 },
                    { new Guid("73634604-a7bd-46d3-8740-1712a1f2b674"), 1.0, "Steak", new Guid("0c98d5db-4983-40cd-923a-df9135ed467e"), 12 },
                    { new Guid("eb3b84dc-7a3d-40d5-bba6-dedf0b7685c2"), 2.0, "Garlic", new Guid("0c98d5db-4983-40cd-923a-df9135ed467e"), 3 },
                    { new Guid("f6e80635-3123-4de7-9165-a9e2a4b4d527"), 1.0, "Pepper", new Guid("0c98d5db-4983-40cd-923a-df9135ed467e"), 0 }
                });

            migrationBuilder.InsertData(
                table: "RecipeTag",
                columns: new[] { "RecipeId", "TagId" },
                values: new object[,]
                {
                    { new Guid("0c98d5db-4983-40cd-923a-df9135ed467e"), new Guid("10386470-5c03-495d-8b43-51437069008e") },
                    { new Guid("0c98d5db-4983-40cd-923a-df9135ed467e"), new Guid("d4977b4a-8679-4350-95d4-baf729080eae") }
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
                name: "IX_RecipeTag_RecipeId",
                table: "RecipeTag",
                column: "RecipeId");

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
