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
                    { new Guid("48cd8f34-0734-455f-ae21-cb9ac843feef"), "#summer" },
                    { new Guid("55fdbfbc-2f46-4b10-83b2-0d4dd6aa54c3"), "#IceCream" },
                    { new Guid("923b0cec-77eb-491e-ab6d-64a9ed877e06"), "#waterProduct" },
                    { new Guid("d4977b4a-8679-4350-95d4-baf729080eae"), "#delicious" }
                });

            migrationBuilder.InsertData(
                table: "PolicyRoles",
                columns: new[] { "Id", "PolicyId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("020a3517-9928-4627-aef1-f823a10d6c33"), new Guid("0fce3dfd-befa-47f7-8a66-07964b6d8f4d"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("03906792-f4a8-4ddd-800c-3c94bbd5625d"), new Guid("ea13f8d7-a3ed-4464-bfbb-f4af8db2b567"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("03eca633-3f2f-472f-9c1a-37544322054c"), new Guid("bf9f8380-023f-4660-9c0f-f8fce5295f48"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("060a2208-8be1-4cc4-9df6-2f7f980b125a"), new Guid("d2194a88-8cb5-41c3-b40d-227ea502c1a8"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("0b4a4cbb-88b4-45ce-a5cb-ad32e6252153"), new Guid("bf9f8380-023f-4660-9c0f-f8fce5295f48"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("1424b859-528a-4893-bec3-9b75de82e7b0"), new Guid("1f1260ac-260e-4bc8-8cb3-a7fa5d8dc366"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("142c8868-f8a4-439f-8e0d-bccf7caffc3f"), new Guid("a80c3363-4977-49f5-9327-1c9b76baa3ed"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("17faa7d2-c6a1-4b38-b57e-5aa849e3c4ca"), new Guid("d787eb55-0b45-45c6-a6fb-0566209831b0"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("1952fee0-8cad-476c-bf99-9e4bc8f7255e"), new Guid("8460422a-5e83-4d24-82a2-eeb45b902406"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("1c353c3f-f879-4c69-80d8-0a40fb2a19d6"), new Guid("1f1260ac-260e-4bc8-8cb3-a7fa5d8dc366"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("1e58469e-93d7-4111-bc4e-383d784f3ab4"), new Guid("cefaeb1f-7ca5-41dd-bd1c-e0b66f5f6051"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("1eb6ea0a-91c4-4600-880b-d527f9cbfc3b"), new Guid("71c436da-3849-4459-8f26-7b299c74162f"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("1edf77ae-b2be-4280-bf0f-596ca9204ea7"), new Guid("8884e0bf-6477-4efc-b5e2-a34fe1d1dda5"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("22f93912-b32b-4624-8a54-f9cae6efa4ce"), new Guid("0fce3dfd-befa-47f7-8a66-07964b6d8f4d"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("27970c60-d187-491b-b012-6a541e050b5f"), new Guid("ea13f8d7-a3ed-4464-bfbb-f4af8db2b567"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("27c1af06-1a2e-43c5-ae21-83f86ae474bc"), new Guid("0fce3dfd-befa-47f7-8a66-07964b6d8f4d"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("2a4bf826-1d56-465e-a13d-084b84721109"), new Guid("d2194a88-8cb5-41c3-b40d-227ea502c1a8"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("2d7f3f0b-2d73-4c3d-867d-5379980c3ec6"), new Guid("0e8e465e-8abd-4c2f-a6a3-3796b0d702bc"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("2ec28125-72c7-4226-a724-6156c0688d6f"), new Guid("65ab361d-916d-43cd-816f-49ed8688bf87"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("30978b4f-9e1b-4086-be56-2e49d160613e"), new Guid("6d966a93-0aa1-420b-bf37-d7288e8e638e"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("39a500a5-5cae-4d42-892d-69dff3a0f88f"), new Guid("5d5a00cb-4c9c-4950-aace-8dd956eaec0c"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("3c07f846-168c-401d-a6cc-4f993279a15c"), new Guid("9b0a1cbc-bb03-48c7-89dd-24b622e6cb84"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("3db13a43-9cb6-4afb-9372-2f33e8e91d14"), new Guid("2b0088ca-6ee6-41a7-b931-ee61f786bd55"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("42a7f7ab-0219-4851-a0dc-0342c2712730"), new Guid("e8fbc57b-4837-4d73-be89-e9f6ce4dc328"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("42d954fa-db54-48fc-881c-714f3c04f254"), new Guid("9b0a1cbc-bb03-48c7-89dd-24b622e6cb84"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("43c01f2a-0da6-4962-9678-e1ea909b123b"), new Guid("2844b8fb-a338-4801-b06b-6f80d923d6c2"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("4713c042-7137-4224-8ed1-0e1cfb4e0782"), new Guid("3eb0b264-294e-4a1d-a748-5610d2a1f1f1"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("4cd44666-c834-461d-99a9-71bf0e45848f"), new Guid("410595d2-43e0-4844-b672-a59a4151d7d5"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("5018860c-5391-4421-9c0d-625a7963589c"), new Guid("3eb0b264-294e-4a1d-a748-5610d2a1f1f1"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("516d3cf4-c1d0-40a1-89fd-d80678d06d0d"), new Guid("d0f9bb82-9abe-4841-ab56-dd79cff023d3"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("5c40dc0d-78dc-4065-802d-a2d4f3597768"), new Guid("2844b8fb-a338-4801-b06b-6f80d923d6c2"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("5f137c47-702e-496c-8412-1f8200224f41"), new Guid("5d5a00cb-4c9c-4950-aace-8dd956eaec0c"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("5f1c8168-8fb4-483c-9776-9bb1720db519"), new Guid("1b68dcb1-d256-4d9e-93f7-51008c268cb9"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("60c2183f-1d48-4d68-9457-c4a50e23d3da"), new Guid("3d041ace-b9ef-425f-b520-136e2dc8c5ba"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("65ec6e27-e8f3-4732-9b11-ceca1a3d3edc"), new Guid("a9401ffb-8d6a-47cd-b9ba-d5d740bb86f8"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("664db5cc-9feb-4d89-beda-1dab6102c70c"), new Guid("8460422a-5e83-4d24-82a2-eeb45b902406"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("665fb1e4-2b7a-42c2-b497-9eeb902de2a6"), new Guid("e8fbc57b-4837-4d73-be89-e9f6ce4dc328"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("6780548a-dd0e-46b7-82e8-f5c52d359d3a"), new Guid("c3001055-9124-4aea-a1ac-2dccb4257512"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("6be14c11-ccb4-49b3-bc74-89516bf26a2d"), new Guid("1d77b3dd-fd7b-4db6-b528-a5c9470378dd"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("6d25085e-b1aa-46c0-8480-319be399f898"), new Guid("0d423b1a-3bc6-48f3-bc9c-c1bb8d5a7071"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("73d96ff5-8a27-4ad8-b5e6-aab52314eff3"), new Guid("a80c3363-4977-49f5-9327-1c9b76baa3ed"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("813572c8-c5a7-4b97-9590-de04c5b27734"), new Guid("d787eb55-0b45-45c6-a6fb-0566209831b0"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("82464c93-8124-441b-842c-ab616abb7e60"), new Guid("384a15ef-8a8f-4152-8c3c-196749f9ab72"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("87ad9fc0-8d05-4e28-b2c9-cda373cecaf7"), new Guid("3d041ace-b9ef-425f-b520-136e2dc8c5ba"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("888a0289-9cff-4e9b-af8d-7ee6f2279021"), new Guid("cefaeb1f-7ca5-41dd-bd1c-e0b66f5f6051"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("889a5d43-bfd1-4641-95a1-86f4e1c1bf41"), new Guid("d6a80b7d-70f3-4a63-a1a4-7ff70c110a49"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("891abd2f-6193-4850-8c13-408d5c057d29"), new Guid("403015ab-9012-42d8-b13b-65366085303a"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("8b4e535a-a443-457d-8156-934e96c3d893"), new Guid("384a15ef-8a8f-4152-8c3c-196749f9ab72"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("8e1292fa-52b4-4ff4-ba56-6c942eac789b"), new Guid("e73cf4bb-4a96-49c2-a3ef-428915e2f968"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("8e56a3fe-dd75-4c46-83dd-52308424f73e"), new Guid("2c497d51-de9d-41a7-b23b-c4f51f6dd9d6"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("8f37c706-8737-4414-a790-dbc75dde503d"), new Guid("e00b12ee-cde2-44e3-ba10-ef4b45390252"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("94a2efec-c2d6-420b-9294-407857119498"), new Guid("1b68dcb1-d256-4d9e-93f7-51008c268cb9"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("95b3e003-6a4c-442c-aaf4-12655d1ec7d6"), new Guid("257400bf-1dbf-4f30-83d7-d1f482651a8d"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("97adb3aa-a062-48f3-8fa8-560626ba7aa8"), new Guid("c3001055-9124-4aea-a1ac-2dccb4257512"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("99ae0209-57c1-4459-a46e-c15c07abfffd"), new Guid("ccb9a20c-6dc2-4c37-a792-33790c42e256"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("9af3adda-cef7-4393-9802-4e25c7721a83"), new Guid("43d2a293-4093-42fd-b80e-c146f5890076"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("9c76de10-24d9-46b2-86d8-9f291d204d34"), new Guid("3d041ace-b9ef-425f-b520-136e2dc8c5ba"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("9d4dc275-0dfd-4880-bdbf-f4bfe13d0c09"), new Guid("8f69170f-064c-40b2-804e-c4b53bb3218d"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("9d786bae-4606-471d-905b-83fb81e07209"), new Guid("ea13f8d7-a3ed-4464-bfbb-f4af8db2b567"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("a02b9c6e-3ee5-4bc6-90e7-64485160ab66"), new Guid("43d2a293-4093-42fd-b80e-c146f5890076"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("a2b93913-9025-43bb-9cd4-a702ed9813f6"), new Guid("ccb9a20c-6dc2-4c37-a792-33790c42e256"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("a348e94c-e33b-4baa-9b36-0abdca93cf5f"), new Guid("6929858c-31ad-40b0-b53d-bc0cc30548ab"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("a6a1fa08-79de-490c-b7b7-66f240d75339"), new Guid("2c497d51-de9d-41a7-b23b-c4f51f6dd9d6"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("a853f390-711d-4f72-afc2-30a24fba4d4d"), new Guid("d5662e2c-f6db-4601-8911-a3faf99d03d2"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("a8e24225-a943-4f6d-a674-8d9109d24b6f"), new Guid("e73cf4bb-4a96-49c2-a3ef-428915e2f968"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("a95688f1-4dc1-495f-ae10-37847b4292ca"), new Guid("0c19f0c5-3da9-435f-b784-81482e074738"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("aa2af1a6-e5a3-4f42-a377-8d8f9cb10385"), new Guid("e8fbc57b-4837-4d73-be89-e9f6ce4dc328"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("ab042645-66cf-4916-ace9-e6f876205593"), new Guid("43d2a293-4093-42fd-b80e-c146f5890076"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("ab59faa9-267c-4819-9acf-c6cbc357c3d1"), new Guid("58b74ad0-74c8-4909-b79b-60d31a57e49b"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("abd44405-fd9b-4d73-b3e7-a46ef8dd1bdd"), new Guid("1381225f-387b-49c9-b57a-6a1a379fdb8a"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("ac885214-f77c-40c0-ad28-50f6b02bb6c8"), new Guid("ec67c7c7-653d-4078-b9ec-82f9d188fd69"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("afaa8029-503f-42e8-9583-cd236c9c90d6"), new Guid("71c436da-3849-4459-8f26-7b299c74162f"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("b1409482-da96-4e2f-82b5-5536a2033be7"), new Guid("97fa4a01-82c8-4ea2-addc-cc5f4037dc1e"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("b18eb196-0203-40e5-885e-b9d53e32a77a"), new Guid("ad057d33-b1ab-491f-a143-a7cd45399a7b"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("b4ae01e2-9f29-4608-a090-bf89a70e2679"), new Guid("0d423b1a-3bc6-48f3-bc9c-c1bb8d5a7071"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("b6468888-df49-49e6-b567-6f290d5e6f46"), new Guid("d6a80b7d-70f3-4a63-a1a4-7ff70c110a49"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("bb1f2f25-2e56-44bf-9b82-ab25724661ef"), new Guid("d2194a88-8cb5-41c3-b40d-227ea502c1a8"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("bcd02ca1-b924-4cd0-a367-ed27c54a1e05"), new Guid("d5662e2c-f6db-4601-8911-a3faf99d03d2"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("beaede9f-90d7-4ea3-81ff-92c402ee9ff5"), new Guid("d0f9bb82-9abe-4841-ab56-dd79cff023d3"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("c2b80fcd-c41b-49ba-967a-129e1824eef7"), new Guid("0c19f0c5-3da9-435f-b784-81482e074738"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("c82ba695-704e-41d9-8c88-dbb93659ff5d"), new Guid("71c436da-3849-4459-8f26-7b299c74162f"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("ca2ed537-852f-4eec-9c7d-19a1fcbef612"), new Guid("2bc75a12-6655-441c-af9d-32be58a5531e"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("d0495e96-d76c-4346-9de0-00db62fd13b9"), new Guid("68d3008d-ede9-4ac8-8eb9-0f8877358476"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("d189e31b-6de3-4e48-9e35-675ed2184bfa"), new Guid("f7d9d48f-62c4-44eb-9ac8-1e7ce066c88d"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("d4b940e8-c633-4e2e-bd7f-19c8121e6414"), new Guid("3eb0b264-294e-4a1d-a748-5610d2a1f1f1"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("d919a66e-b582-44ff-a17f-58f7873ea546"), new Guid("d5662e2c-f6db-4601-8911-a3faf99d03d2"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("dee5d6d9-83ac-4d59-b1a3-167ad73d7316"), new Guid("a9401ffb-8d6a-47cd-b9ba-d5d740bb86f8"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("e0b4125b-7391-4dfb-8992-a0109e603edd"), new Guid("c3001055-9124-4aea-a1ac-2dccb4257512"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("e20b9446-1152-4642-a284-54ecae837b68"), new Guid("257400bf-1dbf-4f30-83d7-d1f482651a8d"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("e2d7bac8-4162-403c-89b5-4f64be90210e"), new Guid("ae6e7487-2375-4e33-911c-74ca4d375f5a"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("e2d8636c-eb4c-4df7-8b0a-a550cb5f89ee"), new Guid("a80c3363-4977-49f5-9327-1c9b76baa3ed"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("e6c6ef3c-2029-4f3e-bce7-f455c1f021d3"), new Guid("5d5a00cb-4c9c-4950-aace-8dd956eaec0c"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("e944165f-2c2b-4ebe-9124-4f2fcd680df8"), new Guid("3b90b27c-5690-4305-a418-3f58efeade35"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("e9e83494-b006-4732-ad38-977133b58573"), new Guid("6f563976-863c-409d-8237-f08ed043ffcd"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("f5179e07-dc17-422b-867e-1692e4a7d379"), new Guid("6f563976-863c-409d-8237-f08ed043ffcd"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("f6f620ba-4c95-4da0-bbfa-28bc0d95261f"), new Guid("9b0a1cbc-bb03-48c7-89dd-24b622e6cb84"), new Guid("24bade73-46d0-40dd-95e7-42352504fe2d") },
                    { new Guid("fe577b71-3cab-41fe-96ec-6a9b27a22847"), new Guid("0d423b1a-3bc6-48f3-bc9c-c1bb8d5a7071"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") },
                    { new Guid("ff5e16f6-f867-4010-8926-cf7813f5553d"), new Guid("6929858c-31ad-40b0-b53d-bc0cc30548ab"), new Guid("3ad84fe7-65f5-44a5-bf99-7727744dfedd") }
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
                values: new object[,]
                {
                    { new Guid("0c98d5db-4983-40cd-923a-df9135ed467e"), "https://iamafoodblog.b-cdn.net/wp-content/uploads/2021/02/how-to-cook-steak-1061w.jpg", 500.0, null, 20, new Guid("d89543a3-0a4a-4830-8092-63c77b049a8f"), "Delicious steak recipe", "Steak Recipe", 15, 2, new Guid("3e80e914-e301-4cb2-9290-206dcefe364c"), "Not available", 2 },
                    { new Guid("bc79385d-89b8-4f48-91a3-dfdfa6f28ebc"), "https://www.iheartnaptime.net/wp-content/uploads/2021/05/IHeartNaptime-easy-homemade-ice-cream-8.jpg", 250.0, null, 0, new Guid("ca79e18d-2f4a-4d3e-8c7d-5b516b9eb516"), "Homemade ice cream recipe", "Ice Cream Recipe", 30, 5, new Guid("1f19e7bd-c9ff-4fd6-8a1d-5e242c5794c4"), "Not available", 5 }
                });

            migrationBuilder.InsertData(
                table: "InstructionSteps",
                columns: new[] { "Id", "RecipeId", "StepDescription", "StepNumber" },
                values: new object[,]
                {
                    { new Guid("06fe49cb-77fd-49ec-abdd-ed92ef74aa32"), new Guid("bc79385d-89b8-4f48-91a3-dfdfa6f28ebc"), "Transfer the mixture to an ice cream maker and churn according to the manufacturer's instructions.", 6 },
                    { new Guid("20b2d8cc-5702-4bdd-8074-330340571e82"), new Guid("0c98d5db-4983-40cd-923a-df9135ed467e"), "Preheat the grill to medium-high heat.", 1 },
                    { new Guid("37598e9b-6fea-469b-acf2-39347b43606a"), new Guid("bc79385d-89b8-4f48-91a3-dfdfa6f28ebc"), "In a medium saucepan, heat the milk and sugar over medium heat until the sugar dissolves.", 1 },
                    { new Guid("52d8fb9a-5704-4454-811c-51176c7951d3"), new Guid("bc79385d-89b8-4f48-91a3-dfdfa6f28ebc"), "Gently fold the whipped cream into the cooled milk mixture.", 5 },
                    { new Guid("72c18da5-05a1-4bb9-8521-0d245262d257"), new Guid("0c98d5db-4983-40cd-923a-df9135ed467e"), "Season the steak with salt and pepper.", 2 },
                    { new Guid("7a4f42ac-c8c5-4a20-b7ed-e8974a56930e"), new Guid("bc79385d-89b8-4f48-91a3-dfdfa6f28ebc"), "In a separate bowl, whip the heavy cream until stiff peaks form.", 4 },
                    { new Guid("a5a18ba6-7e3d-4467-8244-32031abbbc4f"), new Guid("0c98d5db-4983-40cd-923a-df9135ed467e"), "Grill the steak for 4-5 minutes per side for medium-rare.", 3 },
                    { new Guid("adfc2a6e-4ada-455f-9533-8cc3e692587c"), new Guid("bc79385d-89b8-4f48-91a3-dfdfa6f28ebc"), "Once churned, transfer the ice cream to a lidded container and freeze for at least 4 hours or until firm.", 7 },
                    { new Guid("bce890fa-814b-493d-bc7a-678aef54c3e5"), new Guid("bc79385d-89b8-4f48-91a3-dfdfa6f28ebc"), "Pour the mixture into a bowl and let it cool completely.", 3 },
                    { new Guid("d4f9b3a3-f29f-498a-ba39-0c85aa8226fb"), new Guid("bc79385d-89b8-4f48-91a3-dfdfa6f28ebc"), "Remove from heat and stir in the vanilla extract.", 2 }
                });

            migrationBuilder.InsertData(
                table: "RecipeIngredients",
                columns: new[] { "Id", "Amount", "Name", "RecipeId", "Unit" },
                values: new object[,]
                {
                    { new Guid("16e084ef-8990-4c54-b0ee-fc1a9c3f7b44"), 1.0, "Heavy cream", new Guid("bc79385d-89b8-4f48-91a3-dfdfa6f28ebc"), 3 },
                    { new Guid("1db5bca7-e46d-4f12-96b4-bf88d9bef96e"), 2.0, "Garlic", new Guid("0c98d5db-4983-40cd-923a-df9135ed467e"), 3 },
                    { new Guid("25f7cc2b-822e-40e1-93d2-5a769ca76c04"), 0.5, "Sugar", new Guid("bc79385d-89b8-4f48-91a3-dfdfa6f28ebc"), 3 },
                    { new Guid("7cb7d011-0598-4939-a423-d22f0a5b7744"), 1.0, "Salt", new Guid("0c98d5db-4983-40cd-923a-df9135ed467e"), 0 },
                    { new Guid("95bd4ca3-3aee-4b60-bb78-cd881c05c37f"), 1.0, "Steak", new Guid("0c98d5db-4983-40cd-923a-df9135ed467e"), 12 },
                    { new Guid("bf433ca9-3ac3-4f42-953e-a6d3d86d5032"), 1.0, "Vanilla extract", new Guid("bc79385d-89b8-4f48-91a3-dfdfa6f28ebc"), 0 },
                    { new Guid("c7a3a612-e011-4403-844f-a4878905c5ff"), 2.0, "Milk", new Guid("bc79385d-89b8-4f48-91a3-dfdfa6f28ebc"), 3 },
                    { new Guid("cae3b4db-2c51-44b5-b9c2-e0555e2a7f58"), 1.0, "Pepper", new Guid("0c98d5db-4983-40cd-923a-df9135ed467e"), 0 }
                });

            migrationBuilder.InsertData(
                table: "RecipeTag",
                columns: new[] { "RecipeId", "TagId" },
                values: new object[,]
                {
                    { new Guid("0c98d5db-4983-40cd-923a-df9135ed467e"), new Guid("10386470-5c03-495d-8b43-51437069008e") },
                    { new Guid("bc79385d-89b8-4f48-91a3-dfdfa6f28ebc"), new Guid("48cd8f34-0734-455f-ae21-cb9ac843feef") },
                    { new Guid("bc79385d-89b8-4f48-91a3-dfdfa6f28ebc"), new Guid("55fdbfbc-2f46-4b10-83b2-0d4dd6aa54c3") },
                    { new Guid("bc79385d-89b8-4f48-91a3-dfdfa6f28ebc"), new Guid("923b0cec-77eb-491e-ab6d-64a9ed877e06") },
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
