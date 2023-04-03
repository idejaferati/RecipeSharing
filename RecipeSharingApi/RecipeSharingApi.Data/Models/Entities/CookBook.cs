using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeSharingApi.DataLayer.Models.Entities;
public class CookBook
{
    public Guid UserId { get; set; }
    public User User { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public List<Recipe> Recipes { get; set; }
}
