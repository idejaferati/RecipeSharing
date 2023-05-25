using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeSharingApi.BusinessLogic.Services;
using RecipeSharingApi.BusinessLogic.Services.IServices;
using RecipeSharingApi.DataLayer.Models.DTOs.ShoppingList;
using RecipeSharingApi.DataLayer.Models.Entities;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RecipeSharingApi.Controllers
{
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

        /// <summary>
        /// Adds items to the user's shopping list.
        /// </summary>
        /// <param name="shoppingListToCreate">The list of items to add.</param>
        /// <returns>The updated shopping list.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(List<ShoppingListItemCreateDTO>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [Authorize(Policy = "userPolicy")]


        public async Task<ActionResult<List<ShoppingListItemCreateDTO>>> AddToShoppingList(List<ShoppingListItemCreateDTO> shoppingListToCreate)
        {
            var userId = _userService.GetMyId();
            try
            {
                var shoppingList = await _shoppingListService.AddToShoppingList(userId, shoppingListToCreate);
                return Ok(shoppingList);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a shopping list item.
        /// </summary>
        /// <param name="itemId">The ID of the item to delete.</param>
        /// <returns>A boolean indicating the success of the operation.</returns>
        [HttpDelete]
        [Route("{itemId}")]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [Authorize(Policy = "userPolicy")]

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

        /// <summary>
        /// Retrieves a shopping list item by its ID.
        /// </summary>
        /// <param name="shoppingListItemId">The ID of the shopping list item.</param>
        /// <returns>The shopping list item.</returns>
        [HttpGet]
        [Route("{shoppingListItemId}")]
        [ProducesResponseType(typeof(ShoppingListItem), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [Authorize(Policy = "userPolicy")]

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

        /// <summary>
        /// Retrieves the URL link for a shopping list item.
        /// </summary>
        /// <param name="shoppingListItemId">The ID of the shopping list item.</param>
        /// <returns>A redirect to the shopping list item URL.</returns>
        [HttpGet]
        [Route("getlink/{shoppingListItemId}")]
        [ProducesResponseType(typeof(IActionResult), 302)]
        [ProducesResponseType(typeof(string), 404)]
        [Authorize(Policy = "userPolicy")]

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

        /// <summary>
        /// Retrieves the user's shopping list.
        /// </summary>
        /// <returns>The user's shopping list.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<ShoppingListItem>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [Authorize(Policy = "userPolicy")]

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
}
