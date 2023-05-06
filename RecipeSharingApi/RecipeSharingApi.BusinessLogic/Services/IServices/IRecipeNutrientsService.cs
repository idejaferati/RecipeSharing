using RecipeSharingApi.DataLayer.Models.DTOs.Ingredient;
using RecipeSharingApi.DataLayer.Models.DTOs.Nutrients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeSharingApi.BusinessLogic.Services.IServices;
public interface IRecipeNutrientsService
{
    Task<RecipeNutrientsDTO> GetNutrients(List<RecipeIngredientDTO> ingredients);

}
