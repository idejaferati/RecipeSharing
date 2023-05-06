﻿using Newtonsoft.Json;
using RecipeSharingApi.BusinessLogic.Services.IServices;
using RecipeSharingApi.DataLayer.Models.DTOs.Ingredient;
using RecipeSharingApi.DataLayer.Models.DTOs.Nutrients;

namespace RecipeSharingApi.BusinessLogic.Services;
public class RecipeNutrientsService : IRecipeNutrientsService
{
    public async Task<RecipeNutrientsDTO> GetNutrients(List<RecipeIngredientDTO> ingredients)
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("X-Api-Key", "Biy3Eq1ImCLzqyqjVWsTgA==785uoFnkyNXl3iGW");

        var query = "";

        if (ingredients != null && ingredients.Count > 1)
        {
            for (int i = 0; i < ingredients.Count - 1; i++)
            {
                query += $"{ingredients[i].Amount}{ingredients[i].Unit} {ingredients[i].Name} and ";
            }

            query += $"{ingredients[ingredients.Count - 1].Amount}{ingredients[ingredients.Count - 1].Unit} {ingredients[ingredients.Count - 1].Name}";
        }
        else if (ingredients != null && ingredients.Count == 1)
        {
            query += $"{ingredients[0].Amount}{ingredients[0].Unit} {ingredients[0].Name}";
        }
        else
        {
            return null;
        }

        var response = await client.GetStringAsync(new Uri($"https://api.api-ninjas.com/v1/nutrition?query={query}"));

        var serializedResponse = await SerializeResponse(response);

        var result = new RecipeNutrientsDTO();
        serializedResponse.ForEach(ingredientNutrients =>
        {
            result.Calories += ingredientNutrients.Calories;
            result.Protein += ingredientNutrients.Protein_G;
            result.Fiber += ingredientNutrients.Fiber_G;
            result.TotalFat += ingredientNutrients.Fat_Total_G;
            result.TotalCarbohydrates += ingredientNutrients.Carbohydrates_Total_G;
            result.TotalSugar += ingredientNutrients.Sugar_G;
            result.Potassium += ingredientNutrients.Potassium_Mg;
            result.Sodium += ingredientNutrients.Sodium_Mg;
            result.Cholesterol += ingredientNutrients.Cholesterol_Mg;
        });

        return result;
    }

    private async Task<List<NutrientsResponseDTO>> SerializeResponse(string jsonResponse)
    {
        var convertedData = JsonConvert.DeserializeObject<List<NutrientsResponseDTO>>(jsonResponse).ToList();

        return convertedData;
    }
}

