using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using RecipeSharingApi.BusinessLogic.Services;
using RecipeSharingApi.BusinessLogic.Services.IServices;
using RecipeSharingApi.DataLayer.Data.UnitOfWork;
using RecipeSharingApi.DataLayer.Models.DTOs.Tag;
using RecipeSharingApi.DataLayer.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RecipeSharingApi.Test
{
    [TestFixture]
    public class TagServiceTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IMapper> _mapperMock;
        private ITagService _tagService;

        [SetUp]
        public void Setup()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _tagService = new TagService(_unitOfWorkMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task Create_WithValidTagCreateDTO_ReturnsTagDTO()
        {
            // Arrange
            var tagCreateDTO = new TagCreateDTO { Name = "Test Tag" };
            var tag = new Tag { Id = Guid.NewGuid(), Name = tagCreateDTO.Name };

            _unitOfWorkMock
                .Setup(uow => uow.Repository<Tag>().Create(It.IsAny<Tag>()))
                .ReturnsAsync(tag);

            _mapperMock
                .Setup(mapper => mapper.Map<Tag>(tagCreateDTO))
                .Returns(tag);

            _mapperMock
                .Setup(mapper => mapper.Map<TagDTO>(tag))
                .Returns(new TagDTO { Id = tag.Id, Name = tag.Name });

            // Act
            var result = await _tagService.Create(tagCreateDTO);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(tag.Id, result.Id);
            Assert.AreEqual(tag.Name, result.Name);
        }

        [Test]
        public void Create_WithNullTag_ReturnsException()
        {
            // Arrange
            var tagCreateDTO = new TagCreateDTO { Name = "Test Tag" };

            _unitOfWorkMock
                .Setup(uow => uow.Repository<Tag>().Create(It.IsAny<Tag>()))
                .ReturnsAsync((Tag)null);

            // Act & Assert
            Assert.ThrowsAsync<Exception>(() => _tagService.Create(tagCreateDTO));
        }


    }
}