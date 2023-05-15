using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeSharingApi.BusinessLogic.Services;
using RecipeSharingApi.BusinessLogic.Services.IServices;
using RecipeSharingApi.DataLayer.Models.Entities;
using System.Security.Claims;

namespace RecipeSharingApi.Controllers;
[ApiController]
public class RecommendationsController : ControllerBase
{

    private readonly IRecommendationService _recommendationService;
    private readonly ILogger<RecommendationsController> _logger;
    private readonly IUserService _userService;
    public RecommendationsController(IRecommendationService recommendationsService, ILogger<RecommendationsController> logger, IUserService userService)
    {
        _recommendationService = recommendationsService;
        _logger = logger;
        _userService = userService;
    }

    //TODO: Add authorization for needed endpoints


    [HttpGet("GetSingleRecommendation")]
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

    [HttpGet("GetCollectionRecommendation")]
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

