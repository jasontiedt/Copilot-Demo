using ContactsApp.Controllers;
using ContactsApp.Data;
using ContactsApp.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ContactsApp.Tests
{
    public class ContactsControllerTests
    {
        private readonly Mock<IContactRepository> _mockRepo;
        private readonly ContactsController _controller;

        public ContactsControllerTests()
        {
            _mockRepo = new Mock<IContactRepository>();
            _controller = new ContactsController(_mockRepo.Object);
        }

        [Fact]
        public async Task GetContacts_ReturnsOkResult_WithListOfContacts()
        {
            // Arrange
            var contacts = new List<Contact> { new Contact { Id = 1, Name = "Test", Email = "test@email.com" } };
            _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(contacts);

            // Act
            var result = await _controller.GetContacts();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnContacts = Assert.IsType<List<Contact>>(okResult.Value);
            Assert.Single(returnContacts);
        }

        [Fact]
        public async Task GetContactByEmail_ValidEmail_ReturnsOkResult()
        {
            // Arrange
            var contact = new Contact { Id = 1, Name = "Test", Email = "test@email.com" };
            _mockRepo.Setup(repo => repo.GetByEmailAsync("test@email.com")).ReturnsAsync(contact);

            // Act
            var result = await _controller.GetContactByEmail("test@email.com");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnContact = Assert.IsType<Contact>(okResult.Value);
            Assert.Equal("test@email.com", returnContact.Email);
        }

        [Fact]
        public async Task GetContactByEmail_InvalidEmail_ReturnsBadRequest()
        {
            // Act
            var result = await _controller.GetContactByEmail("not-an-email");

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetContactByPhone_ValidPhone_ReturnsOkResult()
        {
            // Arrange
            var contact = new Contact { Id = 2, Name = "Test", Phone = "123-456-7890" };
            _mockRepo.Setup(repo => repo.GetByPhoneAsync("123-456-7890")).ReturnsAsync(contact);

            // Act
            var result = await _controller.GetContactByPhone("123-456-7890");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnContact = Assert.IsType<Contact>(okResult.Value);
            Assert.Equal("123-456-7890", returnContact.Phone);
        }

        [Fact]
        public async Task GetContactByPhone_InvalidPhone_ReturnsBadRequest()
        {
            // Act
            var result = await _controller.GetContactByPhone("badphone");

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task UpdateContactEmail_ValidEmail_ReturnsOkResult()
        {
            // Arrange
            var contact = new Contact { Id = 3, Name = "Test", Email = "new@email.com" };
            _mockRepo.Setup(repo => repo.UpdateEmailAsync(3, "new@email.com")).ReturnsAsync(contact);

            // Act
            var result = await _controller.UpdateContactEmail(3, "new@email.com");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnContact = Assert.IsType<Contact>(okResult.Value);
            Assert.Equal("new@email.com", returnContact.Email);
        }

        [Fact]
        public async Task UpdateContactEmail_InvalidEmail_ReturnsBadRequest()
        {
            // Act
            var result = await _controller.UpdateContactEmail(3, "bademail");

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task UpdateContactPhone_ValidPhone_ReturnsOkResult()
        {
            // Arrange
            var contact = new Contact { Id = 4, Name = "Test", Phone = "321-654-0987" };
            _mockRepo.Setup(repo => repo.UpdatePhoneAsync(4, "321-654-0987")).ReturnsAsync(contact);

            // Act
            var result = await _controller.UpdateContactPhone(4, "321-654-0987");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnContact = Assert.IsType<Contact>(okResult.Value);
            Assert.Equal("321-654-0987", returnContact.Phone);
        }

        [Fact]
        public async Task UpdateContactPhone_InvalidPhone_ReturnsBadRequest()
        {
            // Act
            var result = await _controller.UpdateContactPhone(4, "notaphone");

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
    }
}