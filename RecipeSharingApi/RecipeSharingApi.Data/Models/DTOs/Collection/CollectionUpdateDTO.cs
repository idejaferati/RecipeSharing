﻿using RecipeSharingApi.DataLayer.Models.DTOs.Recipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeSharingApi.DataLayer.Models.DTOs.Collection;
public class CollectionUpdateDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

}
