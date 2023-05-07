using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public ShoppingListController(IShoppingListService shoppingListService)
    {
        _shoppingListService = shoppingListService;
    }

    [HttpPost]
    public async Task<ActionResult<List<ShoppingListItemCreateDTO>>> AddToShoppingList(List<ShoppingListItemCreateDTO> shoppingListToCreate)
    {
        var UserId = new Guid(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
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
        var userId = new Guid(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
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
        var userId = new Guid(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
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
        var userId = new Guid(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
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
        var userId = new Guid(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
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
