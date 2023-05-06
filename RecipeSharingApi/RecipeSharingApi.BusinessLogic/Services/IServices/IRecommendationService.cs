using RecipeSharingApi.DataLayer.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeSharingApi.BusinessLogic.Services.IServices;
public interface IRecommendationService
{
    Task<bool> SetScore(Guid? userId, Guid? tagId);
    Task<bool> SetScore(Guid? userId, Guid? tagId, int? score);
    Task<Recipe> GetSingleRecommendation(Guid? userId);
    Task<List<Recipe>> GetCollectionRecommendations(Guid? userId, int length);
}