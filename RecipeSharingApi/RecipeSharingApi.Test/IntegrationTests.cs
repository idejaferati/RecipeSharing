using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using RecipeSharingApi.BusinessLogic.Services.IServices;
using RecipeSharingApi.Controllers;
using RecipeSharingApi.DataLayer.Models.DTOs.Tag;

namespace RecipeSharingApi.Test
{
    [TestFixture]
    public class IntegrationTests
    {
        private TagController _tagController;
        private Mock<ITagService> _mockTagService;

        [SetUp]
        public void Setup()
        {
            _mockTagService = new Mock<ITagService>();
            _tagController = new TagController(_mockTagService.Object);
        }

        [Test]
        public async Task GetAll_ShouldReturnListOfTags()
        {
            // Arrange
            var expectedTags = new List<TagDTO> { new TagDTO { Id = Guid.NewGuid(), Name = "Tag1" }, new TagDTO { Id = Guid.NewGuid(), Name = "Tag2" } };
            _mockTagService.Setup(service => service.GetAll()).ReturnsAsync(expectedTags);

            // Act
            var result = await _tagController.GetAll();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okObjectResult = (OkObjectResult)result.Result;
            Assert.AreEqual(200, okObjectResult.StatusCode);
            Assert.AreEqual(expectedTags, okObjectResult.Value);
        }

        [Test]
        public async Task Get_WithValidId_ShouldReturnTag()
        {
            // Arrange
            var tagId = Guid.NewGuid();
            var expectedTag = new TagDTO { Id = tagId, Name = "Tag1" };
            _mockTagService.Setup(service => service.Get(tagId)).ReturnsAsync(expectedTag);

            // Act
            var result = await _tagController.Get(tagId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okObjectResult = (OkObjectResult)result.Result;
            Assert.AreEqual(200, okObjectResult.StatusCode);
            Assert.AreEqual(expectedTag, okObjectResult.Value);
        }

        [Test]
        public async Task Create_WithValidTag_ShouldReturnCreatedTag()
        {
            // Arrange
            var tagToCreate = new TagCreateDTO { Name = "Tag1" };
            var createdTag = new TagDTO { Id = Guid.NewGuid(), Name = "Tag1" };
            _mockTagService.Setup(service => service.Create(tagToCreate)).ReturnsAsync(createdTag);

            // Act
            var result = await _tagController.Create(tagToCreate);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okObjectResult = (OkObjectResult)result.Result;
            Assert.AreEqual(200, okObjectResult.StatusCode);
            Assert.AreEqual(createdTag, okObjectResult.Value);
        }

        [Test]
        public async Task Update_WithValidTag_ShouldReturnUpdatedTag()
        {
            // Arrange
            var tagToUpdate = new TagDTO { Id = Guid.NewGuid(), Name = "Tag1" };
            var updatedTag = new TagDTO { Id = tagToUpdate.Id, Name = "TagUpdated" };
            _mockTagService.Setup(service => service.Update(tagToUpdate)).ReturnsAsync(updatedTag);

            // Act
            var result = await _tagController.Update(tagToUpdate);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okObjectResult = (OkObjectResult)result.Result;
            Assert.AreEqual(200, okObjectResult.StatusCode);
            Assert.AreEqual(updatedTag, okObjectResult.Value);
        }

        [Test]
        public async Task Delete_WithValidId_ShouldReturnDeletedTag()
        {
            // Arrange
            var tagId = Guid.NewGuid();
            var deletedTag = new TagDTO { Id = tagId, Name = "Tag1" };
            _mockTagService.Setup(service => service.Delete(tagId)).ReturnsAsync(deletedTag);

            // Act
            var result = await _tagController.Delete(tagId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okObjectResult = (OkObjectResult)result.Result;
            Assert.AreEqual(200, okObjectResult.StatusCode);
            Assert.AreEqual(deletedTag, okObjectResult.Value);
        }
    }
}
