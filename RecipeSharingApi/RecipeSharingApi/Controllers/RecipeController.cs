using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeSharingApi.BusinessLogic.Services;
using RecipeSharingApi.BusinessLogic.Services.IServices;
using RecipeSharingApi.DataLayer.Models.DTOs.Recipe;
using RecipeSharingApi.DataLayer.Models.Entities;
using System.Data;
using System.Security.Claims;

namespace RecipeSharingApi.Controllers;

[ApiController]
[Route("api/recipes")]
public class RecipeController : ControllerBase
{
    private readonly IRecipeService _recipeService;
    private readonly IUserService _userService;
    public RecipeController(IRecipeService recipeService, IUserService userService)
    {
        _recipeService = recipeService;
        _userService = userService;
    }

    //TODO: Add authorization for needed endpoints

    /// <summary>
    /// Creates a new recipe.
    /// </summary>
    /// <param name="recipeToCreate">The recipe data to create.</param>
    /// <returns>The created recipe.</returns>
    [Authorize(Policy = "userPolicy")]
    [ProducesResponseType(typeof(RecipeDTO), 200)]
    [ProducesResponseType(typeof(string), 400)]
    [HttpPost]
    [Authorize(Policy = "userPolicy")]

    public async Task<ActionResult<RecipeDTO>> Create(RecipeCreateDTO recipeToCreate)
    {
        try
        {
            var userId = _userService.GetMyId();
            var recipe = await _recipeService.Create(userId, recipeToCreate);

            return Ok(recipe);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    /// <summary>
    /// Retrieves paginated recipes.
    /// </summary>
    /// <param name="page">The page number.</param>
    /// <param name="pageSize">The number of recipes per page.</param>
    /// <returns>The paginated list of recipes.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<Recipe>), 200)]
    public async Task<ActionResult<List<Recipe>>> GetPaginated(int page, int pageSize)
    {
        List<Recipe> recipes = await _recipeService.GetPaginated(page, pageSize);
        return recipes;
    }

    /// <summary>
    /// Retrieves all recipes.
    /// </summary>
    /// <returns>The list of all recipes.</returns>
    [HttpGet]
    [Route("getAll")]
    [ProducesResponseType(typeof(List<Recipe>), 200)]
    [ProducesResponseType(typeof(string), 404)]
    public async Task<ActionResult<List<Recipe>>> GetAll()
    {
        try
        {
            var recipes = await _recipeService.GetAll();

            return Ok(recipes);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Retrieves a specific recipe by its ID.
    /// </summary>
    /// <param name="id">The ID of the recipe to retrieve.</param>
    /// <returns>The recipe with the specified ID.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Recipe), 200)]
    [ProducesResponseType(typeof(string), 404)]
    public async Task<ActionResult<Recipe>> Get(Guid id)
    {
        try
        {
            var userId = _userService.GetMyId();
            var recipe = await _recipeService.Get(id, userId);

            if (recipe == null)
            {
                return NotFound();
            }

            // Assuming recipeDTO is the same as the recipe object
            var recipeDTO = recipe;

            return Ok(recipeDTO);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Updates an existing recipe.
    /// </summary>
    /// <param name="recipeToUpdate">The updated recipe data.</param>
    /// <returns>The updated recipe.</returns>
    [HttpPut]
    public async Task<ActionResult<Recipe>> Update(RecipeUpdateDTO recipeToUpdate)
    {
        try
        {
            var userId = _userService.GetMyId();
            var recipe = await _recipeService.Update(recipeToUpdate, userId);

            return Ok(recipe);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Retrieves recipes by user ID.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <returns>The list of recipes created by the specified user.</returns>
    [HttpGet("user/{userId}")]
    [ProducesResponseType(typeof(List<Recipe>), 200)]
    [ProducesResponseType(typeof(string), 404)]
    public async Task<ActionResult<List<Recipe>>> GetRecipesByUserId(Guid userId)
    {
        try
        {
            var recipes = await _recipeService.GetRecipesByUserId(userId);
            return Ok(recipes);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    // <summary>
    /// Retrieves the recipes created by the authenticated user.
    /// </summary>
    /// <returns>The list of recipes created by the authenticated user.</returns>
    [HttpGet("user")]
    [Authorize(Policy = "userPolicy")]
    [ProducesResponseType(typeof(List<Recipe>), 200)]
    [ProducesResponseType(typeof(string), 404)]
    public async Task<ActionResult<List<Recipe>>> GetMyRecipes()
    {
        try
        {
            var userId = _userService.GetMyId();
            var recipes = await _recipeService.GetRecipesByUserId(userId);
            return Ok(recipes);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Retrieves recipes by cuisine ID.
    /// </summary>
    /// <param name="cuisineId">The ID of the cuisine.</param>
    /// <returns>The list of recipes associated with the specified cuisine.</returns>
    [HttpGet("cuisine/{cuisineId}")]
    [ProducesResponseType(typeof(List<Recipe>), 200)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetRecipesByCuisineId(Guid cuisineId)
    {
        try
        {
            var recipes = await _recipeService.GetRecipesByCuisineId(cuisineId);
            return Ok(recipes);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<List<Recipe>>> GetRecipesByUserId(Guid userId)
    {
        try
        {
            var recipes = await _recipeService.GetRecipesByUserId(userId);
            return Ok(recipes);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("user")]
    [Authorize(Policy = "userPolicy")]
    public async Task<ActionResult<List<Recipe>>> GetMyRecipes()
    {
        try
        {
            var userId = _userService.GetMyId();
            var recipes = await _recipeService.GetRecipesByUserId(userId);
            return Ok(recipes);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("cuisine/{cuisineId}")]
    public async Task<IActionResult> GetRecipesByCuisineId(Guid cuisineId)
    {
        try
        {
            var recipes = await _recipeService.GetRecipesByCuisineId(cuisineId);
            return Ok(recipes);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while retrieving recipes.");
        }
    }

    /// <summary>
    /// Deletes a recipe by its ID.
    /// </summary>
    /// <param name="id">The ID of the recipe to delete.</param>
    /// <returns>The deleted recipe.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(RecipeDTO), 200)]
    [ProducesResponseType(typeof(string), 404)]
    public async Task<ActionResult<RecipeDTO>> Delete(Guid id)
    {
        try
        {
            var userId = _userService.GetMyId();
            var recipe = await _recipeService.Delete(id, userId);

            return Ok(recipe);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

}