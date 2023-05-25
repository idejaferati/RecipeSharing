using AutoMapper;
using Moq;
using NUnit.Framework;
using RecipeSharingApi.BusinessLogic.Services;
using RecipeSharingApi.DataLayer.Data.UnitOfWork;
using RecipeSharingApi.DataLayer.Models.DTOs.ShoppingList;
using RecipeSharingApi.DataLayer.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeSharingApi.Test
{ 
    [TestFixture]
    public class ShoppingListTests
    {
        private ShoppingListService _shoppingListService;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IMapper> _mapperMock;

        [SetUp]
        public void Setup()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _shoppingListService = new ShoppingListService(_unitOfWorkMock.Object, _mapperMock.Object);
        }


        [Test]
        public void GetShoppingListForUser_NonExistingUserId_ThrowsException()
        {
            // Arrange
            var userId = Guid.NewGuid();

            _unitOfWorkMock
                .Setup(uow => uow.Repository<ShoppingListItem>().GetByCondition(x => x.UserId == userId))
                .Returns(new List<ShoppingListItem>().AsQueryable());

            // Act & Assert
            Assert.ThrowsAsync<Exception>(() => _shoppingListService.GetShoppingListForUser(userId));
        }
    }
}
