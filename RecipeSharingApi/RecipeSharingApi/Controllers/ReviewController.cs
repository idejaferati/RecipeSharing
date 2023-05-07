﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RecipeSharingApi.BusinessLogic.Services.IServices;
using RecipeSharingApi.DataLayer.Models.DTOs.Review;
using System.Security.Claims;

namespace RecipeSharingApi.Controllers;
[ApiController]
[Route("api/reviews")]
public class ReviewController : ControllerBase
{
    public readonly IReviewService _reviewService;
    public ReviewController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [HttpPost]
    public async Task<ActionResult<ReviewDTO>> Create(ReviewCreateDTO reviewToCreate)
    {
        try
        {
            var userId = new Guid(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var review = await _reviewService.Create(userId, reviewToCreate);

            return Ok(review);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
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

    [HttpPut("{id}")]
    public async Task<ActionResult<ReviewDTO>> Update(Guid id, ReviewUpdateDTO reviewToUpdate)
    {
        try
        {
            var userId = new Guid(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var updatedReview = await _reviewService.Update(reviewToUpdate, userId);

            return Ok(updatedReview);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete]
    public async Task<ActionResult<ReviewDTO>> Delete(Guid id)
    {
        try
        {
            var userId = new Guid(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var review = await _reviewService.Delete(id, userId);

            return Ok(review);
        }
        catch (Exception ex) { return NotFound(ex.Message); }
    }
}
