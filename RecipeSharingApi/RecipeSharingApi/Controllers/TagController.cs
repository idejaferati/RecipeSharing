using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeSharingApi.BusinessLogic.Services.IServices;
using RecipeSharingApi.DataLayer.Models.DTOs.Tag;
using RecipeSharingApi.DataLayer.Models.Entities;

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

        /// <summary>
        /// Retrieves all tags.
        /// </summary>
        /// <returns>A list of tags.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<TagDTO>), 200)]
        [ProducesResponseType(typeof(string), 404)]
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

        /// <summary>
        /// Retrieves a specific tag by its ID.
        /// </summary>
        /// <param name="id">The ID of the tag.</param>
        /// <returns>The tag.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TagDTO), 200)]
        [ProducesResponseType(typeof(string), 404)]
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

        /// <summary>
        /// Creates a new tag.
        /// </summary>
        /// <param name="tagToCreate">The tag to create.</param>
        /// <returns>The created tag.</returns>
        [HttpPost]
        [Authorize(Policy = "userPolicy")]
        [ProducesResponseType(typeof(TagDTO), 200)]
        [ProducesResponseType(typeof(string), 400)]
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

        /// <summary>
        /// Updates an existing tag.
        /// </summary>
        /// <param name="tagToUpdate">The tag to update.</param>
        /// <returns>The updated tag.</returns>
        [HttpPut]
        [Authorize(Policy = "adminPolicy")]
        [ProducesResponseType(typeof(TagDTO), 200)]
        [ProducesResponseType(typeof(string), 404)]
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

        /// <summary>
        /// Deletes a tag.
        /// </summary>
        /// <param name="id">The ID of the tag to delete.</param>
        /// <returns>The deleted tag.</returns>
        [HttpDelete]
        [Authorize(Policy = "adminPolicy")]
        [ProducesResponseType(typeof(TagDTO), 200)]
        [ProducesResponseType(typeof(string), 404)]
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
}
