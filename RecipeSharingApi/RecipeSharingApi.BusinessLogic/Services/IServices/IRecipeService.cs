using RecipeSharingApi.DataLayer.Models.DTOs.Nutrients;
using RecipeSharingApi.DataLayer.Models.DTOs.Recipe;
using RecipeSharingApi.DataLayer.Models.Entities;

namespace RecipeSharingApi.BusinessLogic.Services.IServices;
public interface IRecipeService
{
    Task<RecipeDTO> Create(Guid userId, RecipeCreateDTO recipeToCreate);
    Task<RecipeDTO> Get(Guid recipeId, Guid? userId);
    Task<List<Recipe>> GetAll();
    Task<RecipeDTO> Update(RecipeUpdateDTO recipeToUpdate, Guid userId);
    Task<RecipeDTO> Delete(Guid recipeId, Guid userId);
    Task<RecipeNutrientsDTO> GetRecipeNutrients(Guid recipeId);
    Task<Guid> GetRecipeCreatorId(Guid recipeId);
    Task<List<RecipeDTO>> GetPaginated(int page, int pageSize);
}