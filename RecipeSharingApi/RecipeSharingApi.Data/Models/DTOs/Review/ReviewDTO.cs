using RecipeSharingApi.DataLayer.Models.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeSharingApi.DataLayer.Models.DTOs.Review;
public class ReviewDTO
{
    public Guid Id { get; set; }
    public UserDto User { get; set; }
    public double Rating { get; set; }
    public string? Message { get; set; }
}
