using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RecipeSharingApi.BusinessLogic.Services.IServices;
using RecipeSharingApi.DataLayer.Data.UnitOfWork;
using RecipeSharingApi.DataLayer.Models.DTOs.Cuisine;
using RecipeSharingApi.DataLayer.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeSharingApi.BusinessLogic.Services;
public class CuisineService : ICuisineService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CuisineService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CuisineDTO> Create(CuisineCreateDTO cuisineToCreate)
    {
        var cuisine = await _unitOfWork.Repository<Cuisine>().Create(_mapper.Map<Cuisine>(cuisineToCreate));

        if (cuisine is null) throw new Exception("Cuisine could not be created!");

        _unitOfWork.Complete();

        return _mapper.Map<CuisineDTO>(cuisine);
    }

    public async Task<CuisineDTO> Get(Guid cuisineId)
    {
        var cuisine = await _unitOfWork.Repository<Cuisine>().GetByConditionWithIncludes(c => c.Id == cuisineId, "Recipes").FirstOrDefaultAsync();

        if (cuisine is null) throw new Exception("Cuisine not found");

        return _mapper.Map<CuisineDTO>(cuisine);
    }

    public async Task<List<CuisineDTO>> GetAll()
    {
        var cuisines = await _unitOfWork.Repository<Cuisine>().GetAll().ToListAsync();

        if (cuisines is null || cuisines.Count == 0) throw new Exception("Cuisines not found!");

        return _mapper.Map<List<CuisineDTO>>(cuisines);
    }

    public async Task<List<CuisineDTO>> GetPaginated(int page, int pageSize)
    {
        var cuisines = await _unitOfWork.Repository<Cuisine>().GetPaginated(page, pageSize)
           .Include(c => c.Recipes).ToListAsync();

        if (cuisines is null) throw new Exception("Cuisines not found!");

        return _mapper.Map<List<CuisineDTO>>(cuisines);
    }

    public async Task<CuisineDTO> Delete(Guid cuisineId)
    {
        var cuisine = await _unitOfWork.Repository<Cuisine>().GetById(c => c.Id == cuisineId).FirstOrDefaultAsync();

        if (cuisine is null) throw new Exception($"Cuisine not found: {cuisineId}");

        _unitOfWork.Repository<Cuisine>().Delete(cuisine);
        _unitOfWork.Complete();

        return _mapper.Map<CuisineDTO>(cuisine);
    }

    public async Task<CuisineDTO> Update(CuisineUpdateDTO cuisineToUpdate)
    {
        var cuisine = await _unitOfWork.Repository<Cuisine>().GetById(c => c.Id == cuisineToUpdate.Id).FirstOrDefaultAsync();

        if (cuisine is null) throw new Exception($"Cuisine not found:{cuisineToUpdate.Id}");

        cuisine.Name = cuisineToUpdate.Name;

        cuisine = _unitOfWork.Repository<Cuisine>().Update(cuisine);
        if (cuisine is null) throw new Exception("Cusine could not be updated!");
        _unitOfWork.Complete();

        return _mapper.Map<CuisineDTO>(cuisine);
    }
}
