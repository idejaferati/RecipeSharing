using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RecipeSharingApi.BusinessLogic.Services.IServices;
using RecipeSharingApi.DataLayer.Data.UnitOfWork;
using RecipeSharingApi.DataLayer.Models.DTOs.Tag;
using RecipeSharingApi.DataLayer.Models.Entities;
using System.Linq.Expressions;

namespace RecipeSharingApi.BusinessLogic.Services;
public class TagService : ITagService
{
    public readonly IUnitOfWork _unitOfWork;
    public readonly IMapper _mapper;

    public TagService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<TagDTO> Create(TagCreateDTO tagToCreate)
    {
        var tag = await _unitOfWork.Repository<Tag>().Create(_mapper.Map<Tag>(tagToCreate));
        _unitOfWork.Complete();

        if (tag is null) throw new Exception("Tag could not be created!");

        return _mapper.Map<TagDTO>(tag);
    }

    public async Task<List<TagDTO>> GetAll()
    {
        var tags = await _unitOfWork.Repository<Tag>().GetAll().ToListAsync();
        _unitOfWork.Complete();

        if (tags == null) throw new Exception("Tags could not be found!");

        return _mapper.Map<List<TagDTO>>(tags);
    }

    public async Task<TagDTO> Get(Guid tagId)
    {
        Expression<Func<Tag, bool>> expression = x => x.Id == tagId;
        var tag = await _unitOfWork.Repository<Tag>().GetById(expression).FirstOrDefaultAsync();
        _unitOfWork.Complete();

        if (tag == null) throw new Exception("Tag could not be found");

        return _mapper.Map<TagDTO>(tag);
    }

    public async Task<TagDTO> Update(TagDTO tagToUpdate)
    {
        Expression<Func<Tag, bool>> expression = x => x.Id == tagToUpdate.Id;
        var tag = await _unitOfWork.Repository<Tag>().GetById(expression).FirstOrDefaultAsync();

        if (tag is null) throw new Exception("Tag could not be found");

        tag.Name = tagToUpdate.Name;

        _unitOfWork.Repository<Tag>().Update(tag);
        _unitOfWork.Complete();

        return _mapper.Map<TagDTO>(tag);
    }

    public async Task<TagDTO> Delete(Guid id)
    {
        var tag = await _unitOfWork.Repository<Tag>().GetById(x => x.Id == id).FirstOrDefaultAsync();

        if (tag is null) throw new Exception("Tag could not be found");

        _unitOfWork.Repository<Tag>().Delete(tag);
        _unitOfWork.Complete();

        return _mapper.Map<TagDTO>(tag);
    }
}
