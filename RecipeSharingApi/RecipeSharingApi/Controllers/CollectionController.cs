using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecipeSharingApi.BusinessLogic.Services;
using RecipeSharingApi.BusinessLogic.Services.IServices;
using RecipeSharingApi.DataLayer.Models.DTOs.Collection;

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
        public async Task<ActionResult<CollectionDTO>> Create(CollectionCreateRequestDTO collectionToCreateRequest)
        {
            var userId = _userService.GetMyId();
            var collection = await _collectionService.Create(collectionToCreateRequest, userId);

            return CreatedAtAction(nameof(Get), new { id = collection.Id }, collection);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CollectionDTO>>> GetAll()
        {
            var collections = await _collectionService.GetAll();

            return Ok(collections);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CollectionDTO>> Get(Guid id)
        {
            var userId = _userService.GetMyId();
            var collection = await _collectionService.Get(id, userId);

            return Ok(collection);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CollectionDTO>> Update(Guid id, CollectionDTO collectionToUpdate)
        {
            var userId = _userService.GetMyId();
            var collection = await _collectionService.Update(collectionToUpdate, userId);

            if (collection == null)
            {
                return NotFound();
            }

            return Ok(collection);
        }

        //TODO: Update this endpoint to get the userId from the request
        [HttpGet("user")]
        public async Task<ActionResult<IEnumerable<CollectionDTO>>> GetByUserId()
        {
            var userId = _userService.GetMyId();
            var collections = await _collectionService.GetByUserId(userId);

            return Ok(collections);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var userId = _userService.GetMyId();
            var isDeleted = await _collectionService.Delete(id, userId);

            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }

}