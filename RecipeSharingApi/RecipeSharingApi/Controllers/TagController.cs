using Microsoft.AspNetCore.Mvc;
using RecipeSharingApi.BusinessLogic.Services.IServices;
using RecipeSharingApi.DataLayer.Models.DTOs.Tag;

namespace RecipeSharingApi.Controllers;
[ApiController]
[Route("api/tag")]
public class TagController : ControllerBase
{
    private readonly ITagService _tagService;

    public TagController(ITagService tagService)
    {
        _tagService = tagService;
    }

    [HttpGet]
    public async Task<ActionResult<List<TagDTO>>> GetAll()
    {
        try
        {
            var tags = await _tagService.GetAll();

            return Ok(tags);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TagDTO>> Get(Guid id)
    {
        try
        {
            var tag = await _tagService.Get(id);

            return Ok(tag);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<TagDTO>> Create(TagCreateDTO tagToCreate)
    {
        try
        {
            var tag = await _tagService.Create(tagToCreate);

            return Ok(tag);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    public async Task<ActionResult<TagDTO>> Update(TagDTO tagToUpdate)
    {
        try
        {
            var tag = await _tagService.Update(tagToUpdate);

            return Ok(tag);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete]
    public async Task<ActionResult<TagDTO>> Delete(Guid id)
    {
        try
        {
            var tag = await _tagService.Delete(id);

            return Ok(tag);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}
