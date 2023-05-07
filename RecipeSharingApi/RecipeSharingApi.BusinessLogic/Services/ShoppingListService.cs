using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RecipeSharingApi.BusinessLogic.Services.IServices;
using RecipeSharingApi.DataLayer.Data.UnitOfWork;
using RecipeSharingApi.DataLayer.Models.DTOs.ShoppingList;
using RecipeSharingApi.DataLayer.Models.Entities;

namespace RecipeSharingApi.BusinessLogic.Services;
public class ShoppingListService : IShoppingListService
{
    public readonly IUnitOfWork _unitOfWork;
    public readonly IMapper _mapper;

    public ShoppingListService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;

    }
    public async Task<List<ShoppingListItemCreateDTO>> AddToShoppingList(Guid userId, List<ShoppingListItemCreateDTO> shoppingListCreate)
    {
        var shoppingListItems = shoppingListCreate.Select(item => new ShoppingListItemDTO(userId, item)).ToList();
        var entities = _mapper.Map<List<ShoppingListItem>>(shoppingListItems);

        try
        {
            await _unitOfWork.Repository<ShoppingListItem>().CreateRange(entities);
            _unitOfWork.Complete();

            return shoppingListCreate;
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to add shopping list item");
        }
    }
    public async Task<bool> DeleteFromShoppingList(Guid userId, Guid shoppingListItemId)
    {
        var result = false;
        var shoppingListItem = await _unitOfWork.Repository<ShoppingListItem>().GetByCondition(x => x.Id == shoppingListItemId && x.UserId == userId).FirstOrDefaultAsync();

        if (shoppingListItem == null)
        {
            throw new Exception("Shoppinglist item not found");
        }

        try
        {
            _unitOfWork.Repository<ShoppingListItem>().Delete(shoppingListItem);
            result = _unitOfWork.Complete();
            return result;
        }
        catch (Exception ex)
        {
            throw new Exception("ShoppingList item could not be deleted");
        }
    }
    public async Task<ShoppingListItem> GetShoppingListItemById(Guid userId, Guid shoppingListItemId)
    {
        var shoppingListItem = await _unitOfWork.Repository<ShoppingListItem>().GetByCondition(x => x.Id == shoppingListItemId && x.UserId == userId).FirstOrDefaultAsync();

        if (shoppingListItem == null)
        {
            throw new Exception("Shoppinglist item not found");
        }

        return shoppingListItem;
    }

    public async Task<string> GetShoppingListItemUrl(Guid userId, Guid shoppingListItemId)
    {
        var shoppingListItem = await _unitOfWork.Repository<ShoppingListItem>().GetByCondition(x => x.Id == shoppingListItemId && x.UserId == userId).FirstOrDefaultAsync();

        if (shoppingListItem == null)
        {
            throw new Exception("Shoppinglist item not found");
        }

        string groceryShopUrl = "http://www.walmart.com/search?q=";
        string itemName = shoppingListItem.Name;
        string[] itemNameWords = itemName.Split(' ');
        string encodedItemName = string.Join("+", itemNameWords);
        string finalUrl = groceryShopUrl + encodedItemName;

        return finalUrl;
    }
    public async Task<List<ShoppingListItem>> GetShoppingListForUser(Guid userId)
    {
        try
        {
            return await _unitOfWork.Repository<ShoppingListItem>().GetByCondition(x => x.UserId == userId).ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Shoppinglist item not found");
        }
    }


}