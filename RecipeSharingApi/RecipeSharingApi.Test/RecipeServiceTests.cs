using AutoMapper;
using Moq;
using NUnit.Framework;
using RecipeSharingApi.BusinessLogic.Services.IServices;
using RecipeSharingApi.BusinessLogic.Services;
using RecipeSharingApi.DataLayer.Data.UnitOfWork;
using RecipeSharingApi.DataLayer.Models.DTOs.Recipe;
using RecipeSharingApi.DataLayer.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace RecipeSharingApi.Test
{

    [TestFixture]
    public class RecipeServiceTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IMapper> _mapperMock;
        private Mock<IRecommendationService> _recommendationsServiceMock;
        private RecipeService _recipeService;

        [SetUp]
        public void Setup()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _recommendationsServiceMock = new Mock<IRecommendationService>();
            _recipeService = new RecipeService(_unitOfWorkMock.Object, _mapperMock.Object, _recommendationsServiceMock.Object);
        }

        [Test]
        public async Task Create_ValidInputs_ReturnsCreatedRecipe()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var recipeToCreate = new RecipeCreateDTO
            {
                // Set properties for the recipe to be created
            };

            var createdRecipe = new Recipe { /* Set properties for the created recipe */ };
            var createdRecipeDTO = new RecipeDTO { /* Set properties for the created recipe DTO */ };
            var cuisine = new Cuisine { /* Set properties for the cuisine */ };

            _mapperMock.Setup(m => m.Map<Recipe>(recipeToCreate)).Returns(createdRecipe);
            _mapperMock.Setup(m => m.Map<RecipeDTO>(createdRecipe)).Returns(createdRecipeDTO);
            _unitOfWorkMock.Setup(uow => uow.Repository<Cuisine>().GetById(It.IsAny<Guid>())).ReturnsAsync(cuisine);
            _unitOfWorkMock.Setup(uow => uow.Repository<Recipe>().Create(It.IsAny<Recipe>())).ReturnsAsync(createdRecipe);
            _unitOfWorkMock.Setup(uow => uow.Complete()).Returns(1);

            // Act
            var result = await _recipeService.Create(userId, recipeToCreate);

            // Assert
            Assert.AreEqual(createdRecipeDTO, result);
            _unitOfWorkMock.Verify(uow => uow.Repository<Recipe>().Create(It.IsAny<Recipe>()), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Complete(), Times.Once);
        }


    }
}