using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RecipeSharingApi.BusinessLogic.Services;
using RecipeSharingApi.BusinessLogic.Services.IServices;
using RecipeSharingApi.DataLayer.Models.DTOs.Review;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RecipeSharingApi.Controllers
{
    [ApiController]
    [Route("api/reviews")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        private readonly IUserService _userService;

        public ReviewController(IReviewService reviewService, IUserService userService)
        {
            _reviewService = reviewService;
            _userService = userService;
        }

        /// <summary>
        /// Creates a new review for a recipe.
        /// </summary>
        /// <param name="reviewToCreate">The review data.</param>
        /// <returns>The created review.</returns>
        [HttpPost]
        [Authorize(Policy = "userPolicy")]
        [ProducesResponseType(typeof(ReviewDTO), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<ActionResult<ReviewDTO>> Create(ReviewCreateDTO reviewToCreate)
        {
            try
            {
                var userId = _userService.GetMyId();
                var review = await _reviewService.Create(userId, reviewToCreate);

                return Ok(review);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retrieves all reviews for a recipe.
        /// </summary>
        /// <param name="id">The ID of the recipe.</param>
        /// <returns>The list of recipe reviews.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<ReviewDTO>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<ActionResult<List<ReviewDTO>>> GetAll(Guid id)
        {
            try
            {
                var recipeReviews = await _reviewService.GetRecipeReviews(id);

                return Ok(recipeReviews);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Updates a review.
        /// </summary>
        /// <param name="id">The ID of the review.</param>
        /// <param name="reviewToUpdate">The updated review data.</param>
        /// <returns>The updated review.</returns>
        [HttpPut("{id}")]
        [Authorize(Policy = "userPolicy")]
        [ProducesResponseType(typeof(ReviewDTO), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<ActionResult<ReviewDTO>> Update(Guid id, ReviewUpdateDTO reviewToUpdate)
        {
            try
            {
                var userId = _userService.GetMyId();
                var updatedReview = await _reviewService.Update(reviewToUpdate, userId);

                return Ok(updatedReview);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a review.
        /// </summary>
        /// <param name="id">The ID of the review.</param>
        /// <returns>The deleted review.</returns>
        [HttpDelete("{id}")]
        [Authorize(Policy = "userPolicy")]
        [ProducesResponseType(typeof(ReviewDTO), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<ActionResult<ReviewDTO>> Delete(Guid id)
        {
            try
            {
                var userId = _userService.GetMyId();
                var review = await _reviewService.Delete(id, userId);

                return Ok(review);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
