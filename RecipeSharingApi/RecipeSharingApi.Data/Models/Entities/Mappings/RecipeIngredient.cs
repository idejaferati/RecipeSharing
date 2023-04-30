using RecipeSharingApi.DataLayer.Models.Enums;

namespace RecipeSharingApi.DataLayer.Models.Entities.Mappings
{
    public class RecipeIngredient : BaseEntity
    {
        public Guid RecipeId { get; set; }
        public Recipe Recipe { get; set; } // Recipe that the ingredient corresponds to
        public string Name { get; set; }
        public double Amount { get; set; }
        public Unit Unit { get; set; }
    }
}
