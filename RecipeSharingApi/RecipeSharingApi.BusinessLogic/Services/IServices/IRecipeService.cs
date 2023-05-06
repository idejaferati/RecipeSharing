﻿using RecipeSharingApi.DataLayer.Models.DTOs.Nutrients;
using RecipeSharingApi.DataLayer.Models.DTOs.Recipe;

namespace RecipeSharingApi.BusinessLogic.Services.IServices;
public interface IRecipeService
{
    Task<RecipeDTO> Create(Guid userId, RecipeCreateDTO recipeToCreate);
    Task<RecipeDTO> Get(Guid recipeId, Guid? userId);
    Task<List<RecipeDTO>> GetAll();
    Task<RecipeDTO> Update(RecipeUpdateDTO recipeToUpdate, Guid userId);
    Task<RecipeDTO> Delete(Guid recipeId, Guid userId);
    Task<RecipeNutrientsDTO> GetRecipeNutrients(Guid recipeId);
    Task<Guid> GetRecipeCreatorId(Guid recipeId);
    Task<List<RecipeDTO>> GetPaginated(int page, int pageSize);
}