using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeSharingApi.DataLayer.Models.Entities.Mappings;

namespace RecipeSharingApi.DataLayer.Data.EntityConfigurations {
    public class CollectionRecipeEntityTypeConfiguration : IEntityTypeConfiguration<CollectionRecipe> {
        public void Configure(EntityTypeBuilder<CollectionRecipe> builder) {
            //builder.HasKey(k => new {k.CollectionId, k.RecipeId});
        }
    }
}
