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
    private readonly ILogger<RecipeController> _logger;
    private readonly IUserService _userService;
    public RecipeController(IRecipeService recipeService, ILogger<RecipeController> logger, IUserService userService)
    {
        _recipeService = recipeService;
        _logger = logger;
        _userService = userService;
    }

    //TODO: Add authorization for needed endpoints

    [HttpPost]
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
    public async Task<ActionResult<List<RecipeDTO>>> GetPaginated(int page, int pageSize)
    {
        List<RecipeDTO> recipes = await _recipeService.GetPaginated(page, pageSize);
        return recipes;
    }

    [HttpGet]
    [Route("getAll")]
    public async Task<ActionResult<List<RecipeDTO>>> GetAll()
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
    public async Task<ActionResult<RecipeDTO>> Get(Guid id)
    {
        try
        {
            //var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //var guidId = userId is null ? Guid.Empty : new Guid(userId);
            var userId = _userService.GetMyId();
            var recipe = await _recipeService.Get(id, userId);

            return Ok(recipe);
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
        catch (Exception ex) { 
            return NotFound(ex.Message); 
        }
    }

    [HttpPut]
    public async Task<ActionResult<RecipeDTO>> Update(RecipeUpdateDTO recipeToUpdate)
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