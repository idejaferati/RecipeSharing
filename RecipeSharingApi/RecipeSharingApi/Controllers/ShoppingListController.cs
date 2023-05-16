using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeSharingApi.BusinessLogic.Services;
using RecipeSharingApi.BusinessLogic.Services.IServices;
using RecipeSharingApi.DataLayer.Models.DTOs.ShoppingList;
using RecipeSharingApi.DataLayer.Models.Entities;
using System.Security.Claims;

namespace RecipeSharingApi.Controllers;
[ApiController]
[Route("api/ShoppingList")]
public class ShoppingListController : ControllerBase
{
    private readonly IShoppingListService _shoppingListService;
    private readonly IUserService _userService;
    public ShoppingListController(IShoppingListService shoppingListService, IUserService userService)
    {
        _shoppingListService = shoppingListService;
        _userService = userService;
    }

    [HttpPost]
    public async Task<ActionResult<List<ShoppingListItemCreateDTO>>> AddToShoppingList(List<ShoppingListItemCreateDTO> shoppingListToCreate)
    {
        var UserId = _userService.GetMyId();
        try
        {
            var shoppingList = await _shoppingListService.AddToShoppingList(UserId, shoppingListToCreate);
            return Ok(shoppingList);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
    [HttpDelete]
    [Route("{itemId}")]
    public async Task<ActionResult<bool>> DeleteShoppingListItems(Guid itemId)
    {
        var userId = _userService.GetMyId();
        try
        {
            var deletedItems = await _shoppingListService.DeleteFromShoppingList(userId, itemId);
            return Ok(deletedItems);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("{shoppingListItemId}")]
    public async Task<ActionResult<ShoppingListItem>> GetShoppingListItem(Guid shoppingListItemId)
    {
        var userId = _userService.GetMyId();
        try
        {
            var shoppingListItem = await _shoppingListService.GetShoppingListItemById(userId, shoppingListItemId);
            return Ok(shoppingListItem);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet]
    [Route("getlink/{shoppingListItemId}")]
    public async Task<IActionResult> GetShoppingListItemLink(Guid shoppingListItemId)
    {
        var userId = _userService.GetMyId();
        try
        {
            var finalUrl = await _shoppingListService.GetShoppingListItemUrl(userId, shoppingListItemId);
            return Redirect(finalUrl);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }


    [HttpGet]
    public async Task<ActionResult<List<ShoppingListItem>>> GetShoppingList()
    {
        var userId = _userService.GetMyId();
        try
        {
            var shoppingList = await _shoppingListService.GetShoppingListForUser(userId);
            return Ok(shoppingList);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}
