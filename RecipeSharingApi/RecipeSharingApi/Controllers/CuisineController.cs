using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeSharingApi.BusinessLogic.Services.IServices;
using RecipeSharingApi.DataLayer.Models.DTOs.Cuisine;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeSharingApi.Controllers
{
    [ApiController]
    [Route("api/cuisines")]
    public class CuisineController : ControllerBase
    {
        private readonly ICuisineService _cuisineService;

        public CuisineController(ICuisineService cuisineService)
        {
            _cuisineService = cuisineService;
        }

        /// <summary>
        /// Creates a new cuisine.
        /// </summary>
        /// <param name="cuisineToCreate">The cuisine data to create.</param>
        /// <returns>The created cuisine.</returns>
        [HttpPost]
        [Authorize(Policy = "CreateCuisine")]
        [ProducesResponseType(typeof(CuisineDTO), 200)]
        [ProducesResponseType(typeof(string), 400)]
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

        /// <summary>
        /// Retrieves all cuisines.
        /// </summary>
        /// <returns>The list of all cuisines.</returns>
        [HttpGet("all")]
        [ProducesResponseType(typeof(List<CuisineDTO>), 200)]
        [ProducesResponseType(typeof(string), 404)]
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

        /// <summary>
        /// Retrieves a specific cuisine by its ID.
        /// </summary>
        /// <param name="id">The ID of the cuisine to retrieve.</param>
        /// <returns>The cuisine with the specified ID.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CuisineDTO), 200)]
        [ProducesResponseType(typeof(string), 404)]
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

        /// <summary>
        /// Retrieves a paginated list of cuisines.
        /// </summary>
        /// <param name="page">The page number.</param>
        /// <param name="size">The number of items per page.</param>
        /// <returns>The paginated list of cuisines.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<CuisineDTO>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<ActionResult<List<CuisineDTO>>> GetPaginated(int page, int size)
        {
            try
            {
                var cuisines = await _cuisineService.GetPaginated(page, size);
                return Ok(cuisines);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Updates a cuisine.
        /// </summary>
        /// <param name="cuisineToUpdate">The updated cuisine data.</param>
        /// <returns>The updated cuisine.</returns>
        [HttpPut]
        [Authorize(Policy = "UpdateCuisine")]
        [ProducesResponseType(typeof(CuisineDTO), 200)]
        [ProducesResponseType(typeof(string), 400)]
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

        /// <summary>
        /// Deletes a cuisine by its ID.
        /// </summary>
        /// <param name="id">The ID of the cuisine to delete.</param>
        /// <returns>The deleted cuisine.</returns>
        [HttpDelete("{id}")]
        [Authorize(Policy = "DeleteCuisine")]
        [ProducesResponseType(typeof(CuisineDTO), 200)]
        [ProducesResponseType(typeof(string), 404)]
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
}
