using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeSharingApi.BusinessLogic.Services.IServices;
using RecipeSharingApi.DataLayer.Models.DTOs.Cuisine;

namespace RecipeSharingApi.Controllers;
[ApiController]
[Route("api/cuisines")]
public class CuisineController : ControllerBase
{
    private readonly ICuisineService _cuisineService;

    public CuisineController(ICuisineService cuisineService)
    {
        _cuisineService = cuisineService;
    }

    [HttpPost]
    public async Task<ActionResult<CuisineDTO>> Create(CuisineCreateDTO cuisineToCreate)
    {
        try
        {
            var cuisine = await _cuisineService.Create(cuisineToCreate);
            return Ok(cuisine);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("all")]
    public async Task<ActionResult<List<CuisineDTO>>> GetAll()
    {

        try
        {
            var cuisines = await _cuisineService.GetAll();

            return Ok(cuisines);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }

    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CuisineDTO>> Get(Guid id)
    {
        try
        {
            var cuisine = await _cuisineService.Get(id);

            return Ok(cuisine);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<List<CuisineDTO>>> GetPaginated(int page, int size)
    {
        try
        {
            var cuisines = await _cuisineService.GetPaginated(page, size);

            return Ok(cuisines);
        }
        catch(Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPut]
    public async Task<ActionResult<CuisineDTO>> Update(CuisineUpdateDTO cuisineToUpdate)
    {
        try
        {
            var cuisine = await _cuisineService.Update(cuisineToUpdate);
            return Ok(cuisine);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<CuisineDTO>> Delete(Guid id)
    {
        try
        {
            var cuisine = await _cuisineService.Delete(id);
            return Ok(cuisine);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}
