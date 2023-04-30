using RecipeSharingApi.DataLayer.Models.Entities.Mappings;

namespace RecipeSharingApi.DataLayer.Models.Entities;

public class Collection:BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int NumberOfRecipes { get; set; } // Number of recipes within the collection
    public Guid UserId { get; set; } // User who created the collection
    public User User { get; set; }
    public List<Recipe> Recipes { get; set; }
}
