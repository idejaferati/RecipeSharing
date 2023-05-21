using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using RecipeSharingApi.BusinessLogic.Services.IServices;
using RecipeSharingApi.DataLayer.Data.UnitOfWork;
using RecipeSharingApi.DataLayer.Models.DTOs.Review;
using RecipeSharingApi.DataLayer.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeSharingApi.BusinessLogic.Services;
public class ReviewService : IReviewService
{
    public readonly IUnitOfWork _unitOfWork;
    public readonly IMapper _mapper;
    private readonly IRecommendationService _recommendationsService;
    private readonly IRecipeService _recipeService;


    public ReviewService(IUnitOfWork unitOfWork, IMapper mapper, IRecommendationService recommendationsService, IRecipeService recipeService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _recommendationsService = recommendationsService;
        _recipeService = recipeService;
    }

    public async Task<ReviewDTO> Create(Guid userId, ReviewCreateDTO reviewToCreate)
    {
        var review = _mapper.Map<Review>(reviewToCreate);
        review.UserId = userId;

        var recipe = await _unitOfWork.Repository<Recipe>().GetById(recipe => recipe.Id == reviewToCreate.RecipeId).Include(recipe => recipe.Tags).FirstOrDefaultAsync();

        if (recipe is null) throw new Exception("Recipe not found");
        if (recipe.Tags is null || recipe.Tags.Count == 0) throw new Exception("Recipe Tags could not be found!");

        review = await _unitOfWork.Repository<Review>().Create(review);

        if (review is null) throw new Exception("Review could not be created!");

        //create recommendation score
        if (recipe.Tags is null || recipe.Tags.Count == 0) throw new Exception("Recipe Tags could not be found!");
        //set recommendationScore for each tag
        foreach (var tag in recipe.Tags)
        {
            await _recommendationsService.SetScore(review.UserId, tag.Id, (int)review.Rating);
        }
        _unitOfWork.Complete();

        var reviewDTO = _mapper.Map<ReviewDTO>(review);

        return reviewDTO;
    }

    public async Task<List<ReviewDTO>> GetRecipeReviews(Guid recipeId)
    {
        var reviews = await _unitOfWork.Repository<Review>().GetByCondition(r => r.RecipeId == recipeId).ToListAsync();
        var reviewsDTO = _mapper.Map<List<ReviewDTO>>(reviews);
        return reviewsDTO;
    }



    public async Task<ReviewDTO> Update(ReviewUpdateDTO reviewToUpdate, Guid userId)
    {
        var review = await _unitOfWork.Repository<Review>().GetById(x => x.Id == reviewToUpdate.Id && x.UserId == userId).FirstOrDefaultAsync();

        if (review is null) throw new Exception("Review not found");

        review.Rating = reviewToUpdate.Rating;
        review.Message = reviewToUpdate.Message;

        _unitOfWork.Repository<Review>().Update(review);
        _unitOfWork.Complete();

        return _mapper.Map<ReviewDTO>(review);
    }

    public async Task<ReviewDTO> Delete(Guid reviewId, Guid userId)
    {
        var review = await _unitOfWork.Repository<Review>().GetById(x => x.Id == reviewId && x.UserId == userId).FirstOrDefaultAsync();

        if (review == null) throw new Exception("Review not found");

        _unitOfWork.Repository<Review>().Delete(review);
        _unitOfWork.Complete();

        return _mapper.Map<ReviewDTO>(review);
    }
}

