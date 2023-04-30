namespace RecipeSharingApi.DataLayer.Models.Entities.Mappings;

public class RecipeInstruction : BaseEntity
{
    public Guid RecipeId { get; set; } // Recipe that this step corresponds to
    public Recipe Recipe { get; set; }
    public int StepNumber { get; set; }
    public string StepDescription { get; set; }
}
