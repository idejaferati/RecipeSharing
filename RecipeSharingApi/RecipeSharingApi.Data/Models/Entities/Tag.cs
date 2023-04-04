namespace RecipeSharingApi.DataLayer.Models.Entities;
public class Tag : BaseEntity
{
    public string Name { get; set; }

    public List<Recipe> Recipes { get; set; }
}
