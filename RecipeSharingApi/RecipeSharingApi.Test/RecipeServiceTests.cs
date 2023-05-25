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

    }
}