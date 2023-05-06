using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeSharingApi.DataLayer.Models.Entities;
public class RecommendationScore: BaseEntity
{
    public Guid UserId { get; set; }
    public Guid TagId { get; set; }
    public int Score { get; set; }
}
