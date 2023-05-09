
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using RecipeSharingApi.BusinessLogic.Services;
using RecipeSharingApi.BusinessLogic.Services.IServices;
using RecipeSharingApi.Data.Data.UnitOfWork;
using RecipeSharingApi.DataLayer.Data.UnitOfWork;

namespace RecipeSharingApi.BusinessLogic.Helpers {
    public static class StartupHelper
    {       

        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IRecipeService, RecipeService>();
            services.AddTransient<IRecipeNutrientsService, RecipeNutrientsService>();
            services.AddTransient<IRecommendationService,RecommendationsService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();    
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<ICuisineService, CuisineService>();
            services.AddTransient<ICookBookService, CookBookService>();
            services.AddTransient<IReviewService, ReviewService>();
            services.AddTransient<IShoppingListService, ShoppingListService>();
            services.AddTransient<ICollectionService, CollectionService>();

        }
    }
}
