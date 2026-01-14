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
    }
}