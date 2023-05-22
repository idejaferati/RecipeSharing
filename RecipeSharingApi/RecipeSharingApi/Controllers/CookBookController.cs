using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeSharingApi.BusinessLogic.Services;
using RecipeSharingApi.BusinessLogic.Services.IServices;
using RecipeSharingApi.DataLayer.Models.DTOs.CookBook;
using RecipeSharingApi.DataLayer.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeSharingApi.Controllers
{
    [ApiController]
    [Route("api/cookbooks")]
    public class CookBookController : ControllerBase
    {
        private readonly ICookBookService _cookBookService;
        private readonly IUserService _userService;

        public CookBookController(ICookBookService cookBookService, IUserService userService)
        {
            _cookBookService = cookBookService;
            _userService = userService;
        }

        /// <summary>
        /// Creates a new cookbook.
        /// </summary>
        /// <param name="cookBookToCreate">The cookbook data to create.</param>
        /// <returns>The created cookbook.</returns>
        [HttpPost]
        [Authorize(Policy = "userPolicy")]
        [ProducesResponseType(typeof(CookBookDTO), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<ActionResult<CookBookDTO>> Create(CookBookCreateRequestDTO cookBookToCreate)
        {
            try
            {
            var userId = _userService.GetMyId();
            CookBookDTO cookBook;
            
            cookBook = await _cookBookService.Create(cookBookToCreate, userId);
            
                return Ok(cookBook);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retrieves all cookbooks.
        /// </summary>
        /// <returns>The list of all cookbooks.</returns>
        [HttpGet("all")]
        [Authorize(Policy = "adminPolicy")]
        [ProducesResponseType(typeof(List<CookBookDTO>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<ActionResult<List<CookBookDTO>>> GetAll()
        {
            try
            {
                var cookBooks = await _cookBookService.GetAll();
                return Ok(cookBooks);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Retrieves a specific cookbook by its ID.
        /// </summary>
        /// <param name="id">The ID of the cookbook to retrieve.</param>
        /// <returns>The cookbook with the specified ID.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CookBookDTO), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<ActionResult<CookBookDTO>> Get(Guid id)
        {
            try
            {
                var cookBook = await _cookBookService.Get(id);
                return Ok(cookBook);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Retrieves paginated cookbooks.
        /// </summary>
        /// <param name="page">The page number.</param>
        /// <param name="pageSize">The number of cookbooks per page.</param>
        /// <returns>The paginated list of cookbooks.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<CookBookDTO>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<ActionResult<List<CookBookDTO>>> GetPaginated(int page, int pageSize)
        {
            try
            {
            List<CookBookDTO> collections = await _cookBookService.GetPaginated(page, pageSize);

            return collections;
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Updates a cookbook.
        /// </summary>
        /// <param name="cookBookToUpdate">The updated cookbook data.</param>
        /// <returns>The updated cookbook.</returns>
        [HttpPut]
        [Authorize(Policy = "userPolicy")]
        [ProducesResponseType(typeof(CookBookDTO), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<ActionResult<CookBookDTO>> UpdateCookBook(CookBookUpdateDTO cookBookToUpdate)
        {
            try
            {
                var userId = _userService.GetMyId();
                var cookBook = await _cookBookService.Update(cookBookToUpdate, userId);
                return Ok(cookBook);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a cookbook by its ID.
        /// </summary>
        /// <param name="id">The ID of the cookbook to delete.</param>
        /// <returns>The deleted cookbook.</returns>
        [HttpDelete("{id}")]
        [Authorize(Policy = "userPolicy")]
        [ProducesResponseType(typeof(CookBookDTO), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<ActionResult<CookBookDTO>> Delete(Guid id)
        {
            var userId = _userService.GetMyId();
            CookBookDTO cookBook;
            try
            {
                cookBook = await _cookBookService.Delete(id, userId);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            return Ok(cookBook);
        }

        /// <summary>
        /// Adds a recipe to a cookbook.
        /// </summary>
        /// <param name="cookBookId">The ID of the cookbook.</param>
        /// <param name="recipeId">The ID of the recipe.</param>
        /// <returns>The ID of the updated cookbook.</returns>
        [HttpPut("addRecipe")]
        [Authorize(Policy = "userPolicy")]
        [ProducesResponseType(typeof(Guid), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> AddRecipeToCookBook(Guid cookBookId, Guid recipeId)
        {
            try
            {
                var userId = _userService.GetMyId();
                var cookBook = await _cookBookService.AddRecipeToCookBook(userId, cookBookId, recipeId);
                return Ok(cookBookId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Removes a recipe from a cookbook.
        /// </summary>
        /// <param name="cookBookId">The ID of the cookbook.</param>
        /// <param name="recipeId">The ID of the recipe.</param>
        /// <returns>The ID of the updated cookbook.</returns>
        [HttpPut("removeRecipe")]
        [Authorize(Policy = "userPolicy")]
        [ProducesResponseType(typeof(Guid), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> RemoveRecipeFromCookBook(Guid cookBookId, Guid recipeId)
        {
            try
            {
                var userId = _userService.GetMyId();
                var cookBook = await _cookBookService.RemoveRecipeFromCookBook(userId, cookBookId, recipeId);
                return Ok(cookBookId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
