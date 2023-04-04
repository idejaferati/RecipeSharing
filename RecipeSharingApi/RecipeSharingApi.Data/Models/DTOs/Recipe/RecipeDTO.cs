using RecipeSharingApi.DataLayer.Models.Entities;

namespace RecipeSharingApi.DataLayer.Models.DTOs.Recipe;
public class RecipeDTO
{
    public Guid Id { get; set; }
    public User User { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Entities.Cuisine Cuisine { get; set; }
    public List<Entities.Tag> Tags { get; set; }
    public int PrepTime { get; set; }
    public int CookTime { get; set; }
    public int TotalTime { get; set; }
    public List<string> Ingredients { get; set; }
    public List<string> Instructions { get; set; }
    public int Servings { get; set; }
    public int Yield { get; set; }
    public double Calories { get; set; }
    public string AudioInstructions { get; set; } // Audio Url
    public string VideoInstructions { get; set; } // Video Url
}
