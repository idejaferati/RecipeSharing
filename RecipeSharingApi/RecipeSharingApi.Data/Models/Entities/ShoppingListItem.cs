using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeSharingApi.DataLayer.Models.Entities;
public class ShoppingListItem:BaseEntity
{
    public Guid UserId { get; set; }
    public User User { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
}
