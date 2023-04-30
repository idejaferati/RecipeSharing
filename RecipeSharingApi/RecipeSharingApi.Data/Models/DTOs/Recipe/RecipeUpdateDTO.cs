using RecipeSharingApi.DataLayer.Models.Entities;

namespace RecipeSharingApi.DataLayer.Models.DTOs.Recipe;

public class RecipeUpdateDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<string> Ingredients { get; set; }
    public List<string> Instructions { get; set; }
    public List<Entities.Tag> Tags { get; set; }
    public Guid CuisineId { get; set; }
    public int PrepTime { get; set; }
    public int CookTime { get; set; }
    public int Servings { get; set; }
    public int Yield { get; set; }
    public double Calories { get; set; }
    public string AudioInstructions { get; set; } // Audio Url
    public string VideoInstructions { get; set; } // Video Url
}
