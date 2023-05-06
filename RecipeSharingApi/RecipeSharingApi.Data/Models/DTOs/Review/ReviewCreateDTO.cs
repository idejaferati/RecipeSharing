﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeSharingApi.DataLayer.Models.DTOs.Review;
public class ReviewCreateDTO
{
    public Guid RecipeId { get; set; }
    public double Rating { get; set; }
    public string? Message { get; set; }
}
