using RecipeSharingApi.DataLayer.Models.DTOs.ShoppingList;
using RecipeSharingApi.DataLayer.Models.Entities;

namespace RecipeSharingApi.BusinessLogic.Services.IServices;
public interface IShoppingListService
{
    Task<List<ShoppingListItemCreateDTO>> AddToShoppingList(Guid userId, List<ShoppingListItemCreateDTO> shoppingList);
    Task<bool> DeleteFromShoppingList(Guid userId, Guid shoppingListItemIds);
    Task<ShoppingListItem> GetShoppingListItemById(Guid userId, Guid shoppingListItemId);
    Task<string> GetShoppingListItemUrl(Guid userId, Guid shoppingListItemId);
    Task<List<ShoppingListItem>> GetShoppingListForUser(Guid userId);

}