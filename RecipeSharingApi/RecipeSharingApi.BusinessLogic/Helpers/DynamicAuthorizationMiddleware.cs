using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using RecipeSharingApi.DataLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeSharingApi.BusinessLogic.Helpers
{
    public class DynamicAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly RecipeSharingDbContext _dbContext;
        private readonly AuthorizationOptions _authorizationOptions;

        public DynamicAuthorizationMiddleware(RequestDelegate next, RecipeSharingDbContext dbContext, AuthorizationOptions authorizationOptions)
        {
            _next = next;
            _dbContext = dbContext;
            _authorizationOptions = authorizationOptions;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Get the list of policy names from your database or any other source
            var policyNames = new List<string> { "GetNameOfUser", "addrole", "addPolicy", "addRolePolicy", "getRoles",
                "getPolicies", "PostCollections", "getCollections", "getCollectionsById", "putCollections", 
                "getUserCollections", "deleteCollections", "postRecipeInCollections", "RemoveRecipeCollections", 
                "PostCookbooks", "AllCookbooks", "AllUserCookbooks", "GetCookbookById", "AllCookbooksByPage", "UpdateCookBook",
                "DeleteCookbook", "AddRecipeOnCookbook", "DeleteRecipeFromCookbook", "CreateCuisine", "UpdateCuisine", "DeleteCuisine",
                "CreateRecipe", "UpdateRecipe", "GetRecipeByUserId", "GetAllUserRecipe", "DeleteRecipe", "ReviewRecipe", "UpdateReviews",
                "DelteRewiews", "CreateShopingList", "DeleteItemFromShopingList", "GetItemFromShopingListById", "GetLink", "GetUserShopingList", 
                "GetAllTags", "GetATagById", "CreateATag", "UpdateTag", "DeleteTag", "DeleteUser", "AllUsers", "searchByEmail", "ChangePassword", 
                "GetUserById", "UpdateUser", "UpdateUserRole", "MyData" };

            foreach (var policyName in policyNames)
            {
                var roleIds = _dbContext.PolicyRoles
                    .Where(rp => _dbContext.Policies
                        .Where(p => p.Name == policyName)
                        .Select(p => p.Id)
                        .Contains(rp.PolicyId))
                    .Select(rp => rp.RoleId)
                    .ToList();

                var roleStrings = roleIds.ConvertAll(roleId => roleId.ToString());

                if (roleStrings.Count > 0)
                {
                    var policy = new AuthorizationPolicyBuilder()
                        .RequireRole(roleStrings)
                        .Build();

                    _authorizationOptions.AddPolicy(policyName, policy);
                }
            }

            await _next(context);
        }
    }
}
