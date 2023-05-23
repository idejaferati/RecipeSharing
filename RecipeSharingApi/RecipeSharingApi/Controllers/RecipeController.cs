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

    [HttpGet]
    public async Task<ActionResult<List<Recipe>>> GetPaginated(int page, int pageSize)
    {
        List<Recipe> recipes = await _recipeService.GetPaginated(page, pageSize);
        return recipes;
    }

    [HttpGet]
    [Route("getAll")]
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

    [HttpGet("{id}")]
    //[Authorize(AuthenticationSchemes = "Bearer")]
    //[AllowAnonymous]
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


    [HttpGet("{id}/nutrients")]
    public async Task<IActionResult> GetRecipeNutrients(Guid id)
    {
        try
        {
            var result = await _recipeService.GetRecipeNutrients(id);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

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

    [HttpDelete("{id}")]
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