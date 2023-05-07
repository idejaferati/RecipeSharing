using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RecipeSharingApi.BusinessLogic.Services.IServices;
using RecipeSharingApi.DataLayer.Data.UnitOfWork;
using RecipeSharingApi.DataLayer.Models.DTOs.CookBook;
using RecipeSharingApi.DataLayer.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeSharingApi.BusinessLogic.Services;
public class CookBookService : ICookBookService
{
    public readonly IUnitOfWork _unitOfWork;
    public readonly IMapper _mapper;

    public CookBookService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CookBookDTO> Create(CookBookCreateRequestDTO cookBookToCreateRequest, Guid userId)
    {
        CookBookCreateDTO cookBookToCreate = new CookBookCreateDTO(cookBookToCreateRequest, userId);
        var cookBook = _mapper.Map<CookBook>(cookBookToCreate);
        cookBook.Recipes = await _unitOfWork.Repository<Recipe>().GetByCondition(x => cookBookToCreate.Recipes.Contains(x.Id) && x.UserId == userId && x.CookBook == null).ToListAsync();
        if (!cookBook.Recipes.Any()) throw new Exception("None of the recipes mentioned can be in this cookbook");
        cookBook = await _unitOfWork.Repository<CookBook>().Create(cookBook);
        if (cookBook is null) throw new Exception("CookBook could not be created");

        _unitOfWork.Complete();

        var cookBookDTO = _mapper.Map<CookBookDTO>(cookBook);
        cookBookDTO.NumberOfRecipes = cookBook.Recipes.Count;

        return cookBookDTO;
    }

    public async Task<List<CookBookDTO>> GetAll()
    {
        // TODO: Optimize method
        var cookBooks = await _unitOfWork.Repository<CookBook>().GetAll()
            .Include(r => r.Recipes)
            .ThenInclude(r => r.Cuisine)
            .Include(r => r.Recipes)
            .ThenInclude(r => r.Tags)
            .Include(r => r.Recipes)
            .ThenInclude(r => r.Ingredients)
            .Include(r => r.Recipes)
            .ThenInclude(r => r.Instructions).ToListAsync();
        if (cookBooks is null || cookBooks.Count == 0) throw new Exception("Cookbook not found");

        var cookBooksDTO = _mapper.Map<List<CookBookDTO>>(cookBooks);
        cookBooksDTO.ForEach(cookBook =>
        {
            cookBook.NumberOfRecipes = cookBook.Recipes.Count();
        });

        return cookBooksDTO;
    }

    public async Task<CookBookDTO> Get(Guid id)
    {
        var cookBook = await _unitOfWork.Repository<CookBook>().GetById(x => x.Id == id)
            .Include(r => r.Recipes)
            .ThenInclude(r => r.Cuisine)
            .Include(r => r.Recipes)
            .ThenInclude(r => r.Tags)
            .Include(r => r.Recipes)
            .ThenInclude(r => r.Ingredients)
            .Include(r => r.Recipes)
            .ThenInclude(r => r.Instructions).FirstOrDefaultAsync();
        if (cookBook is null) throw new Exception("CookBookNotFound");

        var cookBookDTO = _mapper.Map<CookBookDTO>(cookBook);
        cookBookDTO.NumberOfRecipes = cookBook.Recipes.Count();

        return cookBookDTO;
    }

    public async Task<CookBookDTO> Update(CookBookUpdateDTO cookBookToUpdate, Guid userId)
    {
        var cookBook = _unitOfWork.Repository<CookBook>().GetByCondition(x => x.Id == cookBookToUpdate.Id && x.UserId == userId);

        if (!cookBook.Any()) return null!;

        var cookBookWithIncludes = await cookBook
            .Include(r => r.Recipes)
            .ThenInclude(r => r.Cuisine)
            .Include(r => r.Recipes)
            .ThenInclude(r => r.Tags)
            .Include(r => r.Recipes)
            .ThenInclude(r => r.Ingredients)
            .Include(r => r.Recipes)
            .ThenInclude(r => r.Instructions)
            .Where(x => x.UserId == userId).FirstOrDefaultAsync();
        if (cookBook == null) throw new Exception("User doesn't have access to update this cookBook");

        if (cookBookWithIncludes is null) return null;

        cookBookWithIncludes.Name = cookBookToUpdate.Name;
        cookBookWithIncludes.Description = cookBookToUpdate.Description;


        _unitOfWork.Repository<CookBook>().Update(cookBookWithIncludes);
        _unitOfWork.Complete();
        if (cookBook is null) throw new Exception("CookBook could not be updated!");


        var cookBookDTO = _mapper.Map<CookBookDTO>(cookBookWithIncludes);
        cookBookDTO.NumberOfRecipes = cookBookWithIncludes.Recipes.Count;

        return cookBookDTO;
    }

    public async Task<CookBookDTO> Delete(Guid id, Guid userId)
    {
        var cookBook = await _unitOfWork.Repository<CookBook>().GetById(x => x.Id == id && x.UserId == userId).FirstOrDefaultAsync();

        if (cookBook == null) throw new Exception("Cook Book was not found");

        _unitOfWork.Repository<CookBook>().Delete(cookBook);
        _unitOfWork.Complete();

        var cookBookDTO = _mapper.Map<CookBookDTO>(cookBook);
        cookBookDTO.NumberOfRecipes = cookBook.Recipes.Count;

        return cookBookDTO;
    }

    public async Task<List<CookBookDTO>> GetPaginated(int page, int pageSize)
    {
        var recipes = await _unitOfWork.Repository<CookBook>().GetPaginated(page, pageSize)
           .Include(u => u.User).Include(r => r.Recipes).ToListAsync();

        return _mapper.Map<List<CookBookDTO>>(recipes);
    }

    public async Task<CookBookDTO> AddRecipeToCookBook(Guid userId, Guid cookBookId, Guid recipeId)
    {
        var cookBook = await _unitOfWork.Repository<CookBook>().GetByConditionWithIncludes(c => c.Id == cookBookId && c.UserId == userId, "Recipes").FirstOrDefaultAsync();

        if (cookBook is null) throw new Exception($"CookBook not found: {cookBookId}");

        var recipe = await _unitOfWork.Repository<Recipe>().GetByCondition(r => r.Id == recipeId && r.UserId == userId).FirstOrDefaultAsync();
        if (recipe is null) throw new Exception("CookBookNotFound");

        if (cookBook.Recipes.Contains(recipe))
            throw new Exception($"Recipe already exists in cook book: {recipeId}");

        cookBook.Recipes.Add(recipe);

        cookBook = _unitOfWork.Repository<CookBook>().Update(cookBook);
        _unitOfWork.Complete();

        if (cookBook is null) throw new Exception($"Could not add recipe to cookbook: {recipeId}");

        return _mapper.Map<CookBookDTO>(cookBook);

    }

    public async Task<CookBookDTO> RemoveRecipeFromCookBook(Guid userId, Guid cookBookId, Guid recipeId)
    {
        var cookBook = await _unitOfWork.Repository<CookBook>().GetByConditionWithIncludes(c => c.Id == cookBookId && c.UserId == userId, "Recipes").FirstOrDefaultAsync();

        if (cookBook is null) throw new Exception($"CookBook not found: {cookBookId}");

        var recipeIndex = cookBook.Recipes.FindIndex(x => x.Id == recipeId);

        if (recipeIndex == -1)
        {
            throw new Exception("Recipe Not Found");
        }
        else
        {
            cookBook.Recipes.RemoveAt(recipeIndex);
        }

        cookBook = _unitOfWork.Repository<CookBook>().Update(cookBook);
        _unitOfWork.Complete();

        if (cookBook is null) throw new Exception($"Could not remove recipe from cookbook: {recipeId}");

        return _mapper.Map<CookBookDTO>(cookBook);

    }
}
