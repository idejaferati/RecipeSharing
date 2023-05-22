using RecipeSharingApi.DataLayer.Models.DTOs.Nutrients;
using RecipeSharingApi.DataLayer.Models.DTOs.Recipe;
using RecipeSharingApi.DataLayer.Models.Entities;

namespace RecipeSharingApi.BusinessLogic.Services.IServices;
public interface IRecipeService
{
    Task<RecipeDTO> Create(Guid userId, RecipeCreateDTO recipeToCreate);
    Task<Recipe> Get(Guid recipeId, Guid? user);
    Task<List<Recipe>> GetAll();
    Task<Recipe> Update(RecipeUpdateDTO recipeToUpdate, Guid userId);
    Task<RecipeDTO> Delete(Guid recipeId, Guid userId);
    Task<RecipeNutrientsDTO> GetRecipeNutrients(Guid recipeId);
    Task<Guid> GetRecipeCreatorId(Guid recipeId);
    Task<List<Recipe>> GetPaginated(int page, int pageSize);
    Task<List<Recipe>> GetRecipesByUserId(Guid user);
    Task<List<Recipe>> GetRecipesByCuisineId(Guid cuisineId)

}