using RecipeSharingApi.DataLayer.Models.DTOs.Collection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeSharingApi.BusinessLogic.Services.IServices
{
    public interface ICollectionService
    {
        Task<CollectionDTO> Create(CollectionCreateRequestDTO collectionToCreateRequest, Guid userId);
        Task<List<CollectionDTO>> GetAll();
        Task<CollectionDTO> Get(Guid id, Guid userId);
        Task<CollectionDTO> Update(CollectionUpdateDTO collectionToUpdate, Guid userId);
        Task<List<CollectionDTO>> GetByUserId(Guid userId);
        Task<bool> Delete(Guid id, Guid userId);
        Task<CollectionDTO> AddRecipe(Guid collectionId, Guid recipeId, Guid userId);
        Task<CollectionDTO> RemoveRecipe(Guid collectionId, Guid recipeId, Guid userId);
    }
}
