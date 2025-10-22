using Moq;
using Pubs.API.Data;
using Pubs.API.Services;
using Microsoft.EntityFrameworkCore;
using Pubs.Api.Models;
using Pubs.Api.DTOs;

namespace Pubs.API.Tests
{
    public class PublisherServiceTests
    {
        private readonly Mock<PubsContext> _mockContext;
        private readonly PublisherService _service;
        private readonly List<Publisher> _publishers;

        public PublisherServiceTests()
        {
            _publishers =
            [
                new() { PubId = "1234", PubName = "Test Publisher 1" },
                new() { PubId = "5678", PubName = "Test Publisher 2" }
            ];

            var mockSet = CreateMockDbSet(_publishers);
            _mockContext = new Mock<PubsContext>();
            _mockContext.Setup(m => m.Publishers).Returns(mockSet.Object);

            _service = new PublisherService(_mockContext.Object);
        }

        private static Mock<DbSet<T>> CreateMockDbSet<T>(List<T> elements) where T : class
        {
            var queryable = elements.AsQueryable();
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());
            return mockSet;
        }

        [Fact]
        public async Task GetAllPublishersAsync_ReturnsAllPublishers()
        {
            // Act
            var result = await _service.GetAllPublishersAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetPublisherByIdAsync_ExistingId_ReturnsPublisher()
        {
            // Act
            var result = await _service.GetPublisherByIdAsync("1234");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("1234", result.PubId);
        }

        [Fact]
        public async Task CreatePublisherAsync_ValidData_CreatesPublisher()
        {
            // Arrange
            var createDto = new CreatePublisherDto
            {
                PubId = "9999",
                PubName = "New Publisher",
                City = "City",
                State = "ST",
                Country = "Country"
            };

            // Act
            var result = await _service.CreatePublisherAsync(createDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("9999", result.PubId);
        }
    }
}