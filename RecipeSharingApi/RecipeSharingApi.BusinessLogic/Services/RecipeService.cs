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
using System.Linq;

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
        try
        {
            // Code to create and save the entities
            _unitOfWork.Complete();
        }
        catch (Exception ex)
        {
            // Handle the exception
            var innerException = ex.InnerException;
            // Log or handle the inner exception accordingly
            throw;
        }
        if (recipe is null) throw new Exception("Recipe could not be created!");

        // remove self referencing loops that cause big json values
        recipe.Cuisine.Recipes = null!;
        recipe.Ingredients.ForEach(x => x.Recipe = null!);
        recipe.Tags.ForEach(x => x.Recipes = null!);
        recipe.Instructions.ForEach(x => x.Recipe = null!);

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

    public async Task<Recipe> Get(Guid recipeId, Guid? userId)
    {

            var recipe = await _unitOfWork.Repository<Recipe>()
                .GetByConditionWithIncludes(r => r.Id == recipeId, "User,Cuisine,Tags,Ingredients,Instructions")
                .FirstOrDefaultAsync();

            if (recipe is null)
            {
                throw new Exception("Recipe not found");
            }

            if (userId != null && userId != Guid.Empty)
            {
                await SetScoreOnTags(recipe.Tags, userId);
            }
            recipe.Cuisine.Recipes = null!;
            recipe.Ingredients.ForEach(x => x.Recipe = null!);
            recipe.Tags.ForEach(x => x.Recipes = null!);
            recipe.Instructions.ForEach(x => x.Recipe = null!);
            recipe.User.Recipes = null!;


        return recipe;
    }

    public async Task<List<Recipe>> GetAll()
    {
        var recipes = await _unitOfWork.Repository<Recipe>()
            .GetAll()
            .Include(u => u.User)
            .Include(c => c.Cuisine)
            .Include(t => t.Tags)
            .Include(i => i.Ingredients)
            .Include(i => i.Instructions)
            .ToListAsync();

        if (recipes == null || recipes.Count == 0)
        {
            throw new Exception("Recipes not found");
        }

        foreach (var recipe in recipes)
        {
            recipe.Cuisine.Recipes = null!;
            recipe.Ingredients.ForEach(x => x.Recipe = null!);
            recipe.Tags.ForEach(x => x.Recipes = null!);
            recipe.Instructions.ForEach(x => x.Recipe = null!);
            recipe.User.Recipes = null!;

        }

        return recipes;
    }

    //TODO: Fix the nullable return
    public async Task<RecipeNutrientsDTO> GetRecipeNutrients(Guid recipeId)
    {

        var recipe = await _unitOfWork.Repository<Recipe>()
               .GetByConditionWithIncludes(r => r.Id == recipeId, "User,Cuisine,Tags,Ingredients,Instructions")
               .FirstOrDefaultAsync();
        if (recipe is null) throw new Exception("Recipe not found");

        var nutrients = await _nutrientsService.GetNutrients(_mapper.Map<List<RecipeIngredientDTO>>(recipe.Ingredients));

        if (nutrients is null) throw new Exception("Recipe nutrients not found");

        nutrients.RecipeId = recipe.Id;

        return nutrients;
    }

    public async Task<Recipe> Update(RecipeUpdateDTO recipeToUpdate, Guid userId)
    {
        var recipe = await _unitOfWork.Repository<Recipe>()
               .GetByConditionWithIncludes(r => r.Id == recipeToUpdate.Id && r.UserId == userId, "User,Cuisine,Tags,Ingredients,Instructions")
               .FirstOrDefaultAsync();

        if (recipe == null)
        {
            throw new Exception("Recipe not found");
        }

        // Update the recipe properties with the new values
        recipe.Name = recipeToUpdate.Name;
        recipe.Description = recipeToUpdate.Description;
        recipe.CuisineId = recipeToUpdate.CuisineId;
        recipe.PrepTime = recipeToUpdate.PrepTime;
        recipe.CookTime = recipeToUpdate.CookTime;
        recipe.Servings = recipeToUpdate.Servings;
        recipe.Yield = recipeToUpdate.Yield;
        recipe.Calories = recipeToUpdate.Calories;
        recipe.AudioInstructions = recipeToUpdate.AudioInstructions;
        recipe.VideoInstructions = recipeToUpdate.VideoInstructions;

        // Update the recipe tags
        var existingTags = await _unitOfWork.Repository<Tag>().GetAll().ToListAsync();
        var tagsToCheck = new List<Tag>();

        foreach (var tagDTO in recipeToUpdate.Tags)
        {
            var existingTag = existingTags.FirstOrDefault(t => t.Name == tagDTO.Name);

            if (existingTag != null)
            {
                tagsToCheck.Add(existingTag);
            }
            else
            {
                var newTag = new Tag { Name = tagDTO.Name };
                tagsToCheck.Add(newTag);
                existingTags.Add(newTag);
            }
        }

        recipe.Tags = await AddTagsToRecipe(recipe, tagsToCheck);

        // Update the recipe instructions
        recipe.Instructions.Clear();
        recipe.Instructions.AddRange(recipeToUpdate.Instructions.Select((instruction, index) => new RecipeInstruction
        {
            RecipeId = recipe.Id,
            StepNumber = index + 1,
            StepDescription = instruction
        }));

        // Update the recipe ingredients
        recipe.Ingredients.Clear();
        recipe.Ingredients.AddRange(recipeToUpdate.Ingredients.Select(ingredient => new RecipeIngredient
        {
            RecipeId = recipe.Id,
            Name = ingredient
        }));

        _unitOfWork.Complete();
        recipe.Cuisine.Recipes = null!;
        recipe.Ingredients.ForEach(x => x.Recipe = null!);
        recipe.Tags.ForEach(x => x.Recipes = null!);
        recipe.Instructions.ForEach(x => x.Recipe = null!);
        recipe.User.Recipes = null!;
        return recipe;
    }


    public async Task<RecipeDTO> Delete(Guid recipeId, Guid userId)
    {

        var recipe = await _unitOfWork.Repository<Recipe>().GetById(r => r.Id == recipeId).FirstOrDefaultAsync();

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

    public async Task<List<Recipe>> GetPaginated(int page, int pageSize)
    {
        var recipes = await _unitOfWork.Repository<Recipe>().GetPaginated(page, pageSize)
           .Include(u => u.User)
           .Include(c => c.Cuisine)
           .Include(t => t.Tags)
           .Include(i => i.Ingredients)
           .Include(i => i.Instructions).ToListAsync();
        foreach (var recipe in recipes)
        {
            recipe.Cuisine.Recipes = null!;
            recipe.Ingredients.ForEach(x => x.Recipe = null!);
            recipe.Tags.ForEach(x => x.Recipes = null!);
            recipe.Instructions.ForEach(x => x.Recipe = null!);
            recipe.User.Recipes = null!;

        }

        return recipes;

    }
}