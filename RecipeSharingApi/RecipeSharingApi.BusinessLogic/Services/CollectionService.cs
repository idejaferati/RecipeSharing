using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RecipeSharingApi.BusinessLogic.Services.IServices;
using RecipeSharingApi.DataLayer.Data.UnitOfWork;
using RecipeSharingApi.DataLayer.Models.DTOs.Collection;
using RecipeSharingApi.DataLayer.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeSharingApi.BusinessLogic.Services
{
    public class CollectionService : ICollectionService
    {
        public readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;

        public CollectionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CollectionDTO> Create(CollectionCreateRequestDTO collectionToCreateRequest, Guid userId)
        {
            CollectionCreateDTO collectionToCreate = new CollectionCreateDTO(collectionToCreateRequest, userId);
            var collection = _mapper.Map<Collection>(collectionToCreate);
            collection.Recipes = await _unitOfWork.Repository<Recipe>().GetByCondition(x => collectionToCreate.Recipes.Contains(x.Id) && x.CookBook == null).ToListAsync();
            if (!collection.Recipes.Any()) throw new Exception("None of the recipes mentioned can be in this collection");
            collection = await _unitOfWork.Repository<Collection>().Create(collection);
            if (collection is null) throw new Exception("Collection could not be created");

            _unitOfWork.Complete();

            var collectionDTO = _mapper.Map<CollectionDTO>(collection);
            collectionDTO.NumberOfRecipes = collection.Recipes.Count;

            return collectionDTO;
        }

        public async Task<List<CollectionDTO>> GetAll()
        {
            // TODO: Optimize method
            var collections = await _unitOfWork.Repository<Collection>().GetAll()
                .Include(r => r.Recipes)
                .ThenInclude(r => r.Cuisine)
                .Include(r => r.Recipes)
                .ThenInclude(r => r.Tags)
                .Include(r => r.Recipes)
                .ThenInclude(r => r.Ingredients)
                .Include(r => r.Recipes)
                .ThenInclude(r => r.Instructions).ToListAsync();
            if (collections is null || collections.Count == 0) throw new Exception("Collections not found");

            var collectionsDTO = _mapper.Map<List<CollectionDTO>>(collections);
            collectionsDTO.ForEach(collection =>
            {
                collection.NumberOfRecipes = collection.Recipes.Count();
            });

            return collectionsDTO;
        }

        public async Task<CollectionDTO> Get(Guid id, Guid userId)
        {
            var collection = await _unitOfWork.Repository<Collection>().GetById(x => x.Id == id && x.UserId == userId)
                .Include(r => r.Recipes)
                .ThenInclude(r => r.Cuisine)
                .Include(r => r.Recipes)
                .ThenInclude(r => r.Tags)
                .Include(r => r.Recipes)
                .ThenInclude(r => r.Ingredients)
                .Include(r => r.Recipes)
                .ThenInclude(r => r.Instructions).FirstOrDefaultAsync();
            if (collection is null) throw new Exception("Collection not found");

            var collectionDTO = _mapper.Map<CollectionDTO>(collection);
            collectionDTO.NumberOfRecipes = collection.Recipes.Count();

            return collectionDTO;
        }


        public async Task<CollectionDTO> Update(CollectionDTO collectionToUpdate, Guid userId)
        {
            var collection = _unitOfWork.Repository<Collection>().GetByCondition(x => x.Id == collectionToUpdate.Id && x.UserId == userId);

            if (!collection.Any()) return null!;

            var collectionWithIncludes = await collection
                .Include(r => r.Recipes)
                .ThenInclude(r => r.Cuisine)
                .Include(r => r.Recipes)
                .ThenInclude(r => r.Tags)
                .Include(r => r.Recipes)
                .ThenInclude(r => r.Ingredients)
                .Include(r => r.Recipes)
                .ThenInclude(r => r.Instructions)
                .Where(x => x.UserId == userId).FirstOrDefaultAsync();

            if (collectionWithIncludes == null) throw new Exception("User doesn't have access to update this collection");

            collectionWithIncludes.Name = collectionToUpdate.Name;
            collectionWithIncludes.Description = collectionToUpdate.Description;

            if (collectionToUpdate.Recipes != null)
            {
                collectionWithIncludes.Recipes.Clear();
                foreach (var recipeDto in collectionToUpdate.Recipes)
                {
                    var recipe = await _unitOfWork.Repository<Recipe>().GetByIdAsync(recipeDto.Id);
                    if (recipe != null)
                    {
                        collectionWithIncludes.Recipes.Add(recipe);
                    }
                }
            }

            _unitOfWork.Repository<Collection>().Update(collectionWithIncludes);
            _unitOfWork.Complete();

            var collectionDto = _mapper.Map<CollectionDTO>(collectionWithIncludes);
            collectionDto.NumberOfRecipes = collectionWithIncludes.Recipes.Count;

            return collectionDto;
        }

        public async Task<List<CollectionDTO>> GetByUserId(Guid userId)
        {
            var collections = await _unitOfWork.Repository<Collection>().GetByCondition(x => x.UserId == userId)
                .Include(r => r.Recipes)
                .ThenInclude(r => r.Cuisine)
                .Include(r => r.Recipes)
                .ThenInclude(r => r.Tags)
                .Include(r => r.Recipes)
                .ThenInclude(r => r.Ingredients)
                .Include(r => r.Recipes)
                .ThenInclude(r => r.Instructions).ToListAsync();
            if (collections is null || collections.Count == 0) throw new Exception("Collections not found");

            var collectionsDTO = _mapper.Map<List<CollectionDTO>>(collections);
            collectionsDTO.ForEach(collection =>
            {
                collection.NumberOfRecipes = collection.Recipes.Count();
            });

            return collectionsDTO;
        }

        public async Task<bool> Delete(Guid id, Guid userId)
        {
            var collection = await _unitOfWork.Repository<Collection>().GetByCondition(x => x.Id == id && x.UserId == userId)
                .Include(r => r.Recipes).FirstOrDefaultAsync();
            if (collection is null) throw new Exception("Collection not found");

            if (collection.Recipes.Any())
            {
                foreach (var recipe in collection.Recipes.ToList())
                {
                    collection.Recipes.Remove(recipe);
                }
            }

            _unitOfWork.Repository<Collection>().Delete(collection);
            _unitOfWork.Complete();

            return true;
        }

        public async Task<CollectionDTO> AddRecipe(Guid collectionId, Guid recipeId, Guid userId)
        {
            var collection = await _unitOfWork.Repository<Collection>().GetByCondition(x => x.Id == collectionId && x.UserId == userId)
                .Include(r => r.Recipes).FirstOrDefaultAsync();
            if (collection is null) throw new Exception("Collection not found");

            var recipe = await _unitOfWork.Repository<Recipe>().GetByIdAsync(recipeId);
            if (recipe is null) throw new Exception("Recipe not found");

            if (collection.Recipes.Any(r => r.Id == recipeId)) throw new Exception("Recipe already exists in the collection");

            if (recipe.CookBook != null) throw new Exception("Recipe is already part of another collection");

            collection.Recipes.Add(recipe);
            _unitOfWork.Repository<Collection>().Update(collection);
            _unitOfWork.Complete();

            var collectionDTO = _mapper.Map<CollectionDTO>(collection);
            collectionDTO.NumberOfRecipes = collection.Recipes.Count();

            return collectionDTO;
        }

        public async Task<CollectionDTO> RemoveRecipe(Guid collectionId, Guid recipeId, Guid userId)
        {
            var collection = await _unitOfWork.Repository<Collection>().GetByCondition(x => x.Id == collectionId && x.UserId == userId)
                .Include(r => r.Recipes).FirstOrDefaultAsync();
            if (collection is null) throw new Exception("Collection not found");

            var recipe = collection.Recipes.FirstOrDefault(r => r.Id == recipeId);
            if (recipe is null) throw new Exception("Recipe not found in the collection");

            collection.Recipes.Remove(recipe);
            _unitOfWork.Repository<Collection>().Update(collection);
            _unitOfWork.Complete();

            var collectionDTO = _mapper.Map<CollectionDTO>(collection);
            collectionDTO.NumberOfRecipes = collection.Recipes.Count();

            return collectionDTO;
        }

    }
}
