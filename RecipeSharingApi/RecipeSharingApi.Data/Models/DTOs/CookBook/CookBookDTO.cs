using RecipeSharingApi.DataLayer.Models.DTOs.Recipe;
using RecipeSharingApi.DataLayer.Models.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeSharingApi.DataLayer.Models.DTOs.CookBook;
public class CookBookDTO
{
    public Guid Id { get; set; }
    public UserDto User { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int NumberOfRecipes { get; set; }

    public List<RecipeDTO>? Recipes { get; set; }
}
