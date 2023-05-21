using RecipeSharingApi.DataLayer.Models.DTOs.Tag;

namespace RecipeSharingApi.BusinessLogic.Services.IServices;
public interface ITagService
{
    Task<TagDTO> Create(TagCreateDTO tagToCreate);
    Task<List<TagDTO>> GetAll();
    Task<TagDTO> Get(Guid id);
    Task<TagDTO> Update(TagDTO tagToUpdate);
    Task<TagDTO> Delete(Guid id);
}
