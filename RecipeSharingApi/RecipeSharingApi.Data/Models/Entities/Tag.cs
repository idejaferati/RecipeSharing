using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeSharingApi.DataLayer.Models.Entities;
public class Tag : BaseEntity
{
    public string Name { get; set; }

    public List<Recipe> Recipes { get; set; }
}
