using RecipeSharingApi.DataLayer.Models.Enums;

namespace RecipeSharingApi.DataLayer.Models.DTOs.Ingredient;

public class RecipeIngredientUpdateDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public double Amount { get; set; }
    public Unit Unit { get; set; }
}
