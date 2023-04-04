using Microsoft.AspNetCore.Mvc;
using RecipeSharingApi.BusinessLogic.Services.IServices;
using RecipeSharingApi.DataLayer.Models.DTOs.Recipe;
using RecipeSharingApi.DataLayer.Models.Entities;

namespace RecipeSharingApi.Controllers;

[ApiController]
[Route("api/recipes")]
public class RecipeController : ControllerBase
{
    private readonly IRecipeService _recipeService;
    private readonly ILogger<RecipeController> _logger;
    public RecipeController(IRecipeService recipeService, ILogger<RecipeController> logger)
    {
        _recipeService = recipeService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult<RecipeDTO>> Create(RecipeCreateDTO recipeToCreate)
    {
        try
        {
            var userId = new Guid("6B29FC40-CA47-1067-B31D-00DD010662DA");
            var recipe = await _recipeService.Create(userId, recipeToCreate);

            return Ok(recipe);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        finally
        {
            Console.WriteLine("nuk e di qka u bo");
        }
    }
}