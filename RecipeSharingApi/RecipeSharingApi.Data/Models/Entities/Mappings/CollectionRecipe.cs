namespace RecipeSharingApi.DataLayer.Models.Entities.Mappings;

public class CollectionRecipe : BaseEntity
{
    public Guid CollectionId { get; set; }
    public Collection Collection { get; set; }
    public Guid RecipeId { get; set; }
    public Recipe Recipe { get; set; }
}
