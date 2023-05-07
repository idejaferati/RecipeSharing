using RecipeSharingApi.DataLayer.Models.DTOs.Cuisine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeSharingApi.BusinessLogic.Services.IServices;
public interface ICuisineService
{
    Task<CuisineDTO> Create(CuisineCreateDTO cuisineToCreate);
    Task<CuisineDTO> Get(Guid cuisineId);
    Task<List<CuisineDTO>> GetAll();
    Task<CuisineDTO> Update(CuisineUpdateDTO cuisineToUpdate);
    Task<CuisineDTO> Delete(Guid cuisineId);

    Task<List<CuisineDTO>> GetPaginated(int page, int pageSize);
}