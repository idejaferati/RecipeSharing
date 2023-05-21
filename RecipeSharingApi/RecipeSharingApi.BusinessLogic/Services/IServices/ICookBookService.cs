using RecipeSharingApi.DataLayer.Models.DTOs.CookBook;
using RecipeSharingApi.DataLayer.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeSharingApi.BusinessLogic.Services.IServices;
public interface ICookBookService
{
    Task<CookBookDTO> Create(CookBookCreateRequestDTO cookBookToCreate, Guid userId);
    Task<List<CookBookDTO>> GetAll();
    Task<CookBookDTO> Get(Guid id);
    Task<CookBookDTO> Update(CookBookUpdateDTO cookbookToUpdate, Guid userId);
    Task<CookBookDTO> Delete(Guid id, Guid userId);
    Task<CookBookDTO> AddRecipeToCookBook(Guid userId, Guid cookBookId, Guid recipeId);
    Task<CookBookDTO> RemoveRecipeFromCookBook(Guid userId, Guid cookBookId, Guid recipeId);
    Task<List<CookBookDTO>> GetPaginated(int page, int pageSize);
}
