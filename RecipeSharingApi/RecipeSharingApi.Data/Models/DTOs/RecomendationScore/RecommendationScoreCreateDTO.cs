using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeSharingApi.DataLayer.Models.DTOs.RecomendationScore;
public class RecommendationScoreCreateDto
{
    public Guid UserId { get; set; }
    public Guid TagId { get; set; }
    public int Score { get; set; }
}
