using AutoMapper;
using Moq;
using NUnit.Framework;
using RecipeSharingApi.BusinessLogic.Services.IServices;
using RecipeSharingApi.BusinessLogic.Services;
using RecipeSharingApi.DataLayer.Data.UnitOfWork;
using RecipeSharingApi.DataLayer.Models.DTOs.Cuisine;
using RecipeSharingApi.DataLayer.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeSharingApi.Test
{
    [TestFixture]
    public class CuisineTests
    {

        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IMapper> _mapperMock;
        private ICuisineService _cuisineService;

        [SetUp]
        public void Setup()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _cuisineService = new CuisineService(_unitOfWorkMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task Create_WithValidCuisineCreateDTO_ReturnsCuisineDTO()
        {
            // Arrange
            var cuisineCreateDTO = new CuisineCreateDTO { Name = "Test Cuisine" };
            var cuisine = new Cuisine { Id = Guid.NewGuid(), Name = cuisineCreateDTO.Name };

            _unitOfWorkMock
                .Setup(uow => uow.Repository<Cuisine>().Create(It.IsAny<Cuisine>()))
                .ReturnsAsync(cuisine);

            _mapperMock
                .Setup(mapper => mapper.Map<Cuisine>(cuisineCreateDTO))
                .Returns(cuisine);

            _mapperMock
                .Setup(mapper => mapper.Map<CuisineDTO>(cuisine))
                .Returns(new CuisineDTO { Id = cuisine.Id, Name = cuisine.Name });

            // Act
            var result = await _cuisineService.Create(cuisineCreateDTO);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(cuisine.Id, result.Id);
            Assert.AreEqual(cuisine.Name, result.Name);
        }

        [Test]
        public void Create_WithNullCuisine_ReturnsException()
        {
            // Arrange
            var cuisineCreateDTO = new CuisineCreateDTO { Name = "Test Cuisine" };

            _unitOfWorkMock.Setup(uow => uow.Repository<Cuisine>().Create(It.IsAny<Cuisine>())).ReturnsAsync((Cuisine)null);

            // Act & Assert
            Assert.ThrowsAsync<Exception>(() => _cuisineService.Create(cuisineCreateDTO));
        }


    }
}