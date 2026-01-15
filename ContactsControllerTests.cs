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
            var contacts = new List<Contact> { new Contact { Id = 1, Name = "Test", Email = "test@email.com" } };
            _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(contacts);

            var result = await _controller.GetContacts();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnContacts = Assert.IsType<List<Contact>>(okResult.Value);
            Assert.Single(returnContacts);
        }

        [Fact]
        public async Task GetByEmail_ReturnsOk_WhenContactExists()
        {
            var contact = new Contact { Id = 2, Name = "Jane", Email = "jane@email.com" };
            _mockRepo.Setup(r => r.GetByEmailAsync(contact.Email)).ReturnsAsync(contact);

            var result = await _controller.GetByEmail(contact.Email);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnContact = Assert.IsType<Contact>(okResult.Value);
            Assert.Equal(contact.Email, returnContact.Email);
        }

        [Fact]
        public async Task GetByEmail_ReturnsNotFound_WhenContactDoesNotExist()
        {
            _mockRepo.Setup(r => r.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync((Contact?)null);

            var result = await _controller.GetByEmail("notfound@email.com");

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("notanemail")]
        [InlineData("test@")]
        [InlineData("@domain.com")]
        [InlineData("test@domain")]
        [InlineData("test@domain..com")]
        [InlineData(" ")]
        [InlineData("test@email.com ")]
        [InlineData(" test@email.com")]
        [InlineData("test@EMAIL.com")]
        [InlineData("test@email.comtest@email.comtest@email.comtest@email.comtest@email.comtest@email.comtest@email.comtest@email.comtest@email.comtest@email.com")] // very long
        public async Task GetByEmail_ReturnsBadRequest_ForInvalidOrEdgeCaseEmails(string email)
        {
            var result = await _controller.GetByEmail(email);
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("123")]
        [InlineData("1234567890")]
        [InlineData("123-4567-890")]
        [InlineData("abc-def-ghij")]
        [InlineData("123-456-789")]
        [InlineData("123-456-78900")]
        [InlineData(" 123-456-7890")]
        [InlineData("123-456-7890 ")]
        [InlineData("123-456-789a")]
        [InlineData("   ")]
        public async Task GetByPhone_ReturnsBadRequest_ForInvalidOrEdgeCasePhones(string phone)
        {
            var result = await _controller.GetByPhone(phone);
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetByPhone_ReturnsNotFound_WhenContactDoesNotExist()
        {
            _mockRepo.Setup(r => r.GetByPhoneAsync(It.IsAny<string>())).ReturnsAsync((Contact?)null);
            var result = await _controller.GetByPhone("123-456-7890");
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetByPhone_ReturnsOk_WhenContactExists()
        {
            var contact = new Contact { Id = 5, Name = "Carl", Phone = "123-456-7890" };
            _mockRepo.Setup(r => r.GetByPhoneAsync(contact.Phone)).ReturnsAsync(contact);
            var result = await _controller.GetByPhone(contact.Phone);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnContact = Assert.IsType<Contact>(okResult.Value);
            Assert.Equal(contact.Phone, returnContact.Phone);
        }

        [Fact]
        public async Task UpdatePhone_ReturnsOk_WhenContactUpdated()
        {
            var contact = new Contact { Id = 3, Name = "Bob", Phone = "123-456-7890" };
            _mockRepo.Setup(r => r.UpdatePhoneAsync(contact.Id, contact.Phone)).ReturnsAsync(contact);

            var result = await _controller.UpdatePhone(contact.Id, contact.Phone);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnContact = Assert.IsType<Contact>(okResult.Value);
            Assert.Equal(contact.Phone, returnContact.Phone);
        }

        [Fact]
        public async Task UpdatePhone_ReturnsNotFound_WhenContactDoesNotExist()
        {
            _mockRepo.Setup(r => r.UpdatePhoneAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync((Contact?)null);

            var result = await _controller.UpdatePhone(99, "123-456-7890");

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("123")]
        [InlineData("1234567890")]
        [InlineData("123-4567-890")]
        [InlineData("abc-def-ghij")]
        [InlineData("123-456-789")]
        [InlineData("123-456-78900")]
        [InlineData(" 123-456-7890")]
        [InlineData("123-456-7890 ")]
        [InlineData("123-456-789a")]
        [InlineData("   ")]
        public async Task UpdatePhone_ReturnsBadRequest_ForInvalidOrEdgeCasePhones(string phone)
        {
            var result = await _controller.UpdatePhone(1, phone);
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task UpdateEmail_ReturnsOk_WhenContactUpdated()
        {
            var contact = new Contact { Id = 4, Name = "Alice", Email = "alice@email.com" };
            _mockRepo.Setup(r => r.UpdateEmailAsync(contact.Id, contact.Email)).ReturnsAsync(contact);

            var result = await _controller.UpdateEmail(contact.Id, contact.Email);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnContact = Assert.IsType<Contact>(okResult.Value);
            Assert.Equal(contact.Email, returnContact.Email);
        }

        [Fact]
        public async Task UpdateEmail_ReturnsNotFound_WhenContactDoesNotExist()
        {
            _mockRepo.Setup(r => r.UpdateEmailAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync((Contact?)null);

            var result = await _controller.UpdateEmail(99, "notfound@email.com");

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("notanemail")]
        [InlineData("test@")]
        [InlineData("@domain.com")]
        [InlineData("test@domain")]
        [InlineData("test@domain..com")]
        [InlineData(" ")]
        [InlineData("test@email.com ")]
        [InlineData(" test@email.com")]
        [InlineData("test@EMAIL.com")]
        [InlineData("test@email.comtest@email.comtest@email.comtest@email.comtest@email.comtest@email.comtest@email.comtest@email.comtest@email.comtest@email.com")] // very long
        public async Task UpdateEmail_ReturnsBadRequest_ForInvalidOrEdgeCaseEmails(string email)
        {
            var result = await _controller.UpdateEmail(1, email);
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
    }
}