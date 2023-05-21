using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeSharingApi.BusinessLogic.Services;
using RecipeSharingApi.BusinessLogic.Services.IServices;
using RecipeSharingApi.DataLayer.Models.DTOs.Collection;
using RecipeSharingApi.DataLayer.Models.Entities;

namespace RecipeSharingApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CollectionsController : ControllerBase
    {
        private readonly ICollectionService _collectionService;
        private readonly IUserService _userService;
        public CollectionsController(ICollectionService collectionService, IUserService userService)
        {
            _collectionService = collectionService;
            _userService = userService;
        }

        [HttpPost]
        [Authorize(Policy = "userPolicy")]

        public async Task<ActionResult<CollectionDTO>> Create(CollectionCreateRequestDTO collectionToCreateRequest)
        {
            try
            {
                var userId = _userService.GetMyId();
                var collection = await _collectionService.Create(collectionToCreateRequest, userId);

                return CreatedAtAction(nameof(Get), new { id = collection.Id }, collection);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize(Policy = "adminPolicy")]

        public async Task<ActionResult<IEnumerable<CollectionDTO>>> GetAll()
        {
            try
            {
                var collections = await _collectionService.GetAll();

                return Ok(collections);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "userPolicy")]

        public async Task<ActionResult<CollectionDTO>> Get(Guid id)
        {
            try
            {
                var userId = _userService.GetMyId();
                var collection = await _collectionService.Get(id, userId);

                return Ok(collection);
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut]
        [Authorize(Policy = "userPolicy")]
        public async Task<ActionResult<CollectionDTO>> Update(CollectionUpdateDTO collectionToUpdate)
        {
            try
            {
                var userId = _userService.GetMyId();
                var collection = await _collectionService.Update(collectionToUpdate, userId);

                if (collection == null)
                {
                    return NotFound();
                }

                return Ok(collection);
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        //TODO: Update this endpoint to get the userId from the request
        [HttpGet("user")]
        [Authorize(Policy = "userPolicy")]

        public async Task<ActionResult<IEnumerable<CollectionDTO>>> GetByUserId()
        {
            try
            {
                var userId = _userService.GetMyId();
                var collections = await _collectionService.GetByUserId(userId);

                return Ok(collections);
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "userPolicy")]

        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                var userId = _userService.GetMyId();
                var isDeleted = await _collectionService.Delete(id, userId);

                if (!isDeleted)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("{collectionId}/recipes/addrecipe")]
        [Authorize(Policy = "userPolicy")]
        public async Task<ActionResult<CollectionDTO>> AddRecipeToCollection(Guid collectionId, Guid recipeId)
        {
            try
            {
                var userId = _userService.GetMyId();
                var collection = await _collectionService.AddRecipe(collectionId, recipeId, userId);

                return Ok(collection);
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

    }

}