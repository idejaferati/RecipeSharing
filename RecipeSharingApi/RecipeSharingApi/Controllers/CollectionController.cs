using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeSharingApi.BusinessLogic.Services;
using RecipeSharingApi.BusinessLogic.Services.IServices;
using RecipeSharingApi.DataLayer.Models.DTOs.Collection;
using RecipeSharingApi.DataLayer.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        /// <summary>
        /// Creates a new collection.
        /// </summary>
        /// <param name="collectionToCreateRequest">The collection data to create.</param>
        /// <returns>The created collection.</returns>
        [HttpPost]
        [Authorize(Policy = "userPolicy")]
        [ProducesResponseType(typeof(CollectionDTO), 201)]
        [ProducesResponseType(typeof(string), 400)]
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

        /// <summary>
        /// Retrieves all collections.
        /// </summary>
        /// <returns>The list of all collections.</returns>
        [HttpGet]
        [Authorize(Policy = "adminPolicy")]
        [ProducesResponseType(typeof(IEnumerable<CollectionDTO>), 200)]
        [ProducesResponseType(typeof(string), 400)]
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

        /// <summary>
        /// Retrieves a specific collection by its ID.
        /// </summary>
        /// <param name="id">The ID of the collection to retrieve.</param>
        /// <returns>The collection with the specified ID.</returns>
        [HttpGet("{id}")]
        [Authorize(Policy = "userPolicy")]
        [ProducesResponseType(typeof(CollectionDTO), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<ActionResult<CollectionDTO>> Get(Guid id)
        {
            try
            {
                var userId = _userService.GetMyId();
                var collection = await _collectionService.Get(id, userId);
                return Ok(collection);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Updates a collection.
        /// </summary>
        /// <param name="collectionToUpdate">The updated collection data.</param>
        /// <returns>The updated collection.</returns>
        [HttpPut]
        [Authorize(Policy = "userPolicy")]
        [ProducesResponseType(typeof(CollectionDTO), 200)]
        [ProducesResponseType(typeof(string), 404)]
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
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Retrieves collections associated with the authenticated user.
        /// </summary>
        /// <returns>The list of collections associated with the user.</returns>
        [HttpGet("user")]
        [Authorize(Policy = "userPolicy")]
        [ProducesResponseType(typeof(IEnumerable<CollectionDTO>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<ActionResult<IEnumerable<CollectionDTO>>> GetByUserId()
        {
            try
            {
                var userId = _userService.GetMyId();
                var collections = await _collectionService.GetByUserId(userId);
                return Ok(collections);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a collection by its ID.
        /// </summary>
        /// <param name="id">The ID of the collection to delete.</param>
        /// <returns>No content if the collection was deleted successfully.</returns>
        [HttpDelete("{id}")]
        [Authorize(Policy = "userPolicy")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(string), 404)]
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
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Adds a recipe to a collection.
        /// </summary>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="recipeId">The ID of the recipe.</param>
        /// <returns>The updated collection.</returns>
        [HttpPost("{collectionId}/recipes/addrecipe")]
        [Authorize(Policy = "userPolicy")]
        [ProducesResponseType(typeof(CollectionDTO), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<ActionResult<CollectionDTO>> AddRecipeToCollection(Guid collectionId, Guid recipeId)
        {
            try
            {
                var userId = _userService.GetMyId();
                var collection = await _collectionService.AddRecipe(collectionId, recipeId, userId);
                return Ok(collection);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
