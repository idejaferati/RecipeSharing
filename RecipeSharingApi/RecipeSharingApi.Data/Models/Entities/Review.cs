using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeSharingApi.DataLayer.Models.Entities;
public class Review:BaseEntity
{
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid RecipeId { get; set; }
    public Recipe Recipe { get; set; }
    public double Rating { get; set; }
    public string? Message { get; set; }
}
