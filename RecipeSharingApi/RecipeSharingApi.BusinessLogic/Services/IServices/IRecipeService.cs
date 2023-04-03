using RecipeSharingApi.DataLayer.Models.DTOs.Recipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeSharingApi.BusinessLogic.Services.IServices;
public interface IRecipeService
{
    Task<RecipeDTO> Create(Guid userId, RecipeCreateDTO recipeToCreate);
    Task<RecipeDTO> Get(Guid recipeId);
    Task<List<RecipeDTO>> GetAll();
    Task<RecipeDTO> Update(RecipeUpdateDTO recipeToUpdate);
    Task<RecipeDTO> Delete(Guid recipeId);

    Task<Guid> GetRecipeCreatorId(Guid recipeId);
    Task<List<RecipeDTO>> GetPaginated(int page, int pageSize);
}
