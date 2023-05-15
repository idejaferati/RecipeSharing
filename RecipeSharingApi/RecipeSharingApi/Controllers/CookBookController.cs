using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeSharingApi.BusinessLogic.Services;
using RecipeSharingApi.BusinessLogic.Services.IServices;
using RecipeSharingApi.DataLayer.Models.DTOs.CookBook;
using RecipeSharingApi.DataLayer.Models.Entities;
using System.Security.Claims;

namespace RecipeSharingApi.Controllers;
[ApiController]
[Route("api/cookbooks")]
public class CookBookController : ControllerBase
{
    private readonly ICookBookService _cookBookService;
    private readonly IUserService _userService;


    public CookBookController(ICookBookService cookBookService, IUserService userService ,ILogger<RecipeController> logger)
    {
        _cookBookService = cookBookService;
        _userService = userService;

    }

    [HttpPost]
    [Authorize(Policy= "onlyadmin")]
    public async Task<ActionResult<CookBookDTO>> Create(CookBookCreateRequestDTO cookBookToCreate)
    {
        try
        {
            var UserId = _userService.GetMyId();
            CookBookDTO cookBook;
            
            cookBook = await _cookBookService.Create(cookBookToCreate, UserId);
            
            return Ok(cookBook);
        }
        catch (Exception ex) 
        { 
            return BadRequest(ex.Message); 
        }
    }

    [HttpGet("all")]
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

    [HttpGet("{id}")]
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

    [HttpGet]
    public async Task<ActionResult<List<CookBookDTO>>> GetPaginated(int page, int pageSize)
    {
        List<CookBookDTO> collections = await _cookBookService.GetPaginated(page, pageSize);

        return collections;
    }

    [HttpPut]
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

    [HttpDelete("{id}")]
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

    [HttpPut("addRecipe")]
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

    [HttpPut("removeRecipe")]
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

