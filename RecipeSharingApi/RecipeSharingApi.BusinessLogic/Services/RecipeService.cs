using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RecipeSharingApi.BusinessLogic.Helpers.Extensions;
using RecipeSharingApi.BusinessLogic.Services.IServices;
using RecipeSharingApi.DataLayer.Data.UnitOfWork;
using RecipeSharingApi.DataLayer.Models.DTOs.Recipe;
using RecipeSharingApi.DataLayer.Models.Entities;

namespace RecipeSharingApi.BusinessLogic.Services;
public class RecipeService:IRecipeService
{
    public readonly IUnitOfWork _unitOfWork;
    public readonly IMapper _mapper;

    public RecipeService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public Task<RecipeDTO> Create(Guid userId, RecipeCreateDTO recipeToCreate)
    {
        throw new NotImplementedException();
    }

    public Task<RecipeDTO> Delete(Guid recipeId)
    {
        throw new NotImplementedException();
    }

    public Task<RecipeDTO> Get(Guid recipeId)
    {
        throw new NotImplementedException();
    }

    public Task<List<RecipeDTO>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<List<RecipeDTO>> GetPaginated(int page, int pageSize)
    {
        throw new NotImplementedException();
    }

    public Task<Guid> GetRecipeCreatorId(Guid recipeId)
    {
        throw new NotImplementedException();
    }

    public Task<RecipeDTO> Update(RecipeUpdateDTO recipeToUpdate)
    {
        throw new NotImplementedException();
    }

    private async Task<List<Tag>> AddTagsToRecipe(Recipe recipe, List<Tag> tagsToCreate)
    {
        var existingTags = await _unitOfWork.Repository<Tag>().GetAll().ToListAsync();

        // Check if any of the tags is missing in database when assigned to the recipe on creation
        // create the ones that are missing, and assign the existing ones accordingly

        var missingTags = tagsToCreate.Except(existingTags, new TagNameComparer()).ToList();
        if (missingTags.Any())
        {
            await _unitOfWork.Repository<Tag>().CreateRange(missingTags);
        }

        existingTags.AddRange(missingTags);

        var tagsToAdd = new List<Tag>();
        existingTags.ForEach(async recipeTag =>
        {
            if (tagsToCreate.Contains(recipeTag, new TagNameComparer()))
            {
                tagsToAdd.Add(recipeTag);

            }
        });

        return tagsToAdd;
    }
}
