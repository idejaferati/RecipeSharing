using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeSharingApi.BusinessLogic.Services;
using RecipeSharingApi.BusinessLogic.Services.IServices;
using RecipeSharingApi.DataLayer.Models.Entities;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RecipeSharingApi.Controllers
{
    [ApiController]
    public class RecommendationsController : ControllerBase
    {
        private readonly IRecommendationService _recommendationService;
        private readonly IUserService _userService;

        public RecommendationsController(IRecommendationService recommendationsService, IUserService userService)
        {
            _recommendationService = recommendationsService;
            _userService = userService;
        }

        /// <summary>
        /// Retrieves a single recipe recommendation for the authenticated user.
        /// </summary>
        /// <returns>The recommended recipe.</returns>
        [HttpGet("GetSingleRecommendation")]
        [ProducesResponseType(typeof(Recipe), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<ActionResult<Recipe>> GetSingleRecommendation()
        {
            try
            {
                var userId = _userService.GetMyId();
                var recommendation = await _recommendationService.GetSingleRecommendation(userId);

                return Ok(recommendation);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Retrieves a collection of recipe recommendations for the authenticated user.
        /// </summary>
        /// <param name="length">The number of recommendations to retrieve.</param>
        /// <returns>The recommended recipe collection.</returns>
        [HttpGet("GetCollectionRecommendation")]
        [ProducesResponseType(typeof(List<Recipe>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<ActionResult<List<Recipe>>> GetCollectionRecommendations(int length)
        {
            try
            {
                var userId = _userService.GetMyId();
                var recommendationCollection = await _recommendationService.GetCollectionRecommendations(userId, length);

                return Ok(recommendationCollection);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
