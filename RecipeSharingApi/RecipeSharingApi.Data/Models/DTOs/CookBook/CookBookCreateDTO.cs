﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeSharingApi.DataLayer.Models.DTOs.CookBook;
public class CookBookCreateDTO : CookBookCreateRequestDTO
{
    public Guid UserId { get; set; }
    public CookBookCreateDTO(CookBookCreateRequestDTO requestDTO, Guid userId)
    {
        UserId = userId;
        Name = requestDTO.Name;
        Description = requestDTO.Description;
        Recipes = requestDTO.Recipes;
    }
}
public class CookBookCreateRequestDTO
{
    public string Name { get; set; }
    public string Description { get; set; }

    public List<Guid> Recipes { get; set; }
}