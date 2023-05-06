using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RecipeSharingApi.BusinessLogic.Helpers.Extensions;
using RecipeSharingApi.BusinessLogic.Services.IServices;
using RecipeSharingApi.DataLayer.Data.UnitOfWork;
using RecipeSharingApi.DataLayer.Models.DTOs.Ingredient;
using RecipeSharingApi.DataLayer.Models.DTOs.Nutrients;
using RecipeSharingApi.DataLayer.Models.DTOs.Recipe;
using RecipeSharingApi.DataLayer.Models.Entities;
using RecipeSharingApi.DataLayer.Models.Entities.Mappings;

namespace RecipeSharingApi.BusinessLogic.Services;
public class RecipeService : IRecipeService
{
    public readonly IUnitOfWork _unitOfWork;
    public readonly IMapper _mapper;
    public readonly IRecipeNutrientsService _nutrientsService;
    private readonly IRecommendationService _recommendationsService;
    public RecipeService(IUnitOfWork unitOfWork, IMapper mapper, IRecipeNutrientsService nutrientsService, IRecommendationService recommendationsService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _nutrientsService = nutrientsService;
        _recommendationsService = recommendationsService;
       
    }

    public async Task<RecipeDTO> Create(Guid userId, RecipeCreateDTO recipeToCreate)
    {
        var recipe = _mapper.Map<Recipe>(recipeToCreate);
        recipe.UserId = userId;

        var tagsToCheck = new List<Tag>();
        recipeToCreate.Tags.ForEach(t => tagsToCheck.Add(new Tag { Name = t.Name }));

        recipe.Ingredients.ForEach(x => x.RecipeId = recipe.Id);
        recipe.Instructions.ForEach(x => x.RecipeId = recipe.Id);
        recipe.Tags = await AddTagsToRecipe(recipe, tagsToCheck);

        recipe.Cuisine = await _unitOfWork.Repository<Cuisine>().GetById(x => x.Id == recipe.CuisineId).FirstOrDefaultAsync();
        if (recipe.Cuisine is null) throw new Exception("Cuisine not found!");

        recipe = await _unitOfWork.Repository<Recipe>().Create(recipe);
        _unitOfWork.Complete();

        if (recipe is null) throw new Exception("Recipe could not be created!");

        // remove self referencing loops that cause big json values
        recipe.Cuisine.Recipes = null!;
        recipe.Ingredients.ForEach(x => x.Recipe = null!);
        recipe.Tags.ForEach(x => x.Recipes = null!);
        recipe.Instructions.ForEach(x => x.Recipe = null!);
        recipe.User.Recipes = null!;


        var recipeDTO = _mapper.Map<RecipeDTO>(recipe);

        return recipeDTO;
    }

    private async Task SetScoreOnTags(List<Tag> tags, Guid? userId)
    {
        foreach (Tag tag in tags)
        {
            //create a recommendation score
            await _recommendationsService.SetScore(userId, tag.Id);
        }
    }

    public async Task<RecipeDTO> Get(Guid recipeId, Guid? userId)
    {

        var recipe = await _unitOfWork.Repository<Recipe>().GetByConditionWithIncludes(recipe => recipe.Id == recipeId, "User, Ingredients, Instructions, Tags, Cuisine").FirstOrDefaultAsync();

        if (recipe is null) throw new Exception("Recipe not found");

        //create a recommendation score for each tag in recipe
        if (userId != null && userId != Guid.Empty)
        {
            await SetScoreOnTags(recipe.Tags, userId);
        }

        var singleRecipe = _mapper.Map<RecipeDTO>(recipe);

        return singleRecipe;
    }

    public async Task<List<RecipeDTO>> GetAll()
    {

        var recipes = await _unitOfWork.Repository<Recipe>().GetAll()
           .Include(u => u.User)
           .Include(c => c.Cuisine)
           .Include(t => t.Tags)
           .Include(i => i.Ingredients)
           .Include(i => i.Instructions).ToListAsync();

        if (recipes is null || recipes.Count == 0) throw new Exception("Recipes not found");

        var recipesToReturn = _mapper.Map<List<RecipeDTO>>(recipes);

        return recipesToReturn;
    }

    public async Task<RecipeNutrientsDTO> GetRecipeNutrients(Guid recipeId)
    {

        var recipe = await _unitOfWork.Repository<Recipe>().GetByConditionWithIncludes(x => x.Id == recipeId, "User, Ingredients, Instructions, Tags, Cuisine").FirstOrDefaultAsync();

        if (recipe is null) throw new Exception("Recipe not found");

        var nutrients = await _nutrientsService.GetNutrients(_mapper.Map<List<RecipeIngredientDTO>>(recipe.Ingredients));

        if (nutrients is null) throw new Exception("Recipe nutrients not found");

        nutrients.RecipeId = recipe.Id;

        return nutrients;
    }

    public async Task<RecipeDTO> Update(RecipeUpdateDTO recipeToUpdate, Guid userId)
    {
        var recipe = await _unitOfWork.Repository<Recipe>().GetByConditionWithIncludes(x => x.Id == recipeToUpdate.Id && x.UserId == userId, "User, Ingredients, Instructions, Tags, Cuisine").FirstOrDefaultAsync();

        if (recipe == null) throw new Exception("Recipe not found");

        recipe.Name = recipeToUpdate.Name;
        recipe.Description = recipeToUpdate.Description;

        recipe.Ingredients = _mapper.Map<List<RecipeIngredient>>(recipeToUpdate.Ingredients);
        recipe.Ingredients.ForEach(x => { x.RecipeId = recipe.Id; x.Recipe = recipe; });

        recipe.Instructions = _mapper.Map<List<RecipeInstruction>>(recipeToUpdate.Instructions);
        recipe.Instructions.ForEach(x => { x.RecipeId = recipe.Id; x.Recipe = recipe; });

        //TODO: Update tags

        recipe.VideoInstructions = recipeToUpdate.VideoInstructions;
        recipe.AudioInstructions = recipeToUpdate.AudioInstructions;

        recipe = _unitOfWork.Repository<Recipe>().Update(recipe);
        _unitOfWork.Complete();

        if (recipe is null) throw new Exception("Recipe could not be updated!");

        // remove self referencing loops that cause big json values
        recipe.User.Recipes = null!;
        recipe.Ingredients.ForEach(x => x.Recipe = null!);
        recipe.Instructions.ForEach(x => x.Recipe = null!);
        recipe.Tags.ForEach(x => x.Recipes = null!);
        recipe.Cuisine.Recipes = null!;

        var updatedRecipe = _mapper.Map<RecipeDTO>(recipe);
        return updatedRecipe;
    }

    public async Task<RecipeDTO> Delete(Guid recipeId, Guid userId)
    {
        var recipe = await _unitOfWork.Repository<Recipe>().GetById(r => r.Id == recipeId && r.UserId == userId).FirstOrDefaultAsync();

        if (recipe == null) throw new Exception("Recipe not found");

        _unitOfWork.Repository<Recipe>().Delete(recipe);
        _unitOfWork.Complete();

        return _mapper.Map<RecipeDTO>(recipe);
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

                //create a recommendation score
                await _recommendationsService.SetScore(recipe.UserId, recipeTag.Id);
            }
        });

        return tagsToAdd;
    }

    public async Task<Guid> GetRecipeCreatorId(Guid id)
    {
        var recipe = await _unitOfWork.Repository<Recipe>().GetByCondition(x => x.Id == id).FirstOrDefaultAsync();

        if (recipe is null) throw new Exception("Recipe not found");

        return recipe.UserId;
    }

    public async Task<List<RecipeDTO>> GetPaginated(int page, int pageSize)
    {
        var recipes = await _unitOfWork.Repository<Recipe>().GetPaginated(page, pageSize)
           .Include(u => u.User)
           .Include(c => c.Cuisine)
           .Include(t => t.Tags)
           .Include(i => i.Ingredients)
           .Include(i => i.Instructions).ToListAsync();
        return _mapper.Map<List<RecipeDTO>>(recipes);
    }
}