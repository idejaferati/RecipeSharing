using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeSharingApi.DataLayer.Models.DTOs.ShoppingList;
public class ShoppingListItemDTO : ShoppingListItemCreateDTO
{
    public Guid UserId { get; set; }
    public ShoppingListItemDTO(Guid userId, ShoppingListItemCreateDTO item)
    {
        UserId = userId;
        Name = item.Name;
        Quantity = item.Quantity;
    }
}
public class ShoppingListItemCreateDTO
{
    public string Name { get; set; }
    public int Quantity { get; set; }
}