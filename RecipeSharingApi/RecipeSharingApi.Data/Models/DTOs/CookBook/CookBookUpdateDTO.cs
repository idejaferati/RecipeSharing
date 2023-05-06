using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeSharingApi.DataLayer.Models.DTOs.CookBook;
public class CookBookUpdateDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}
