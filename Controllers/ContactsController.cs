using ContactsApp.Data;
using ContactsApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactsApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactRepository _repository;

        public ContactsController(IContactRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> GetContacts()
        {
            var contacts = await _repository.GetAllAsync();
            return Ok(contacts);
        }

        [HttpGet("by-phone/{phone}")]
        public async Task<ActionResult<Contact>> GetByPhone(string phone)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(phone, @"^\d{3}-\d{3}-\d{4}$"))
            {
                return BadRequest("Phone number must be in the format xxx-xxx-xxxx");
            }
            var contact = await _repository.GetByPhoneAsync(phone);
            if (contact == null) return NotFound();
            return Ok(contact);
        }

        [HttpGet("by-email/{email}")]
        public async Task<ActionResult<Contact>> GetByEmail(string email)
        {
            var emailValidator = new System.ComponentModel.DataAnnotations.EmailAddressAttribute();
            if (!emailValidator.IsValid(email))
            {
                return BadRequest("Invalid email format");
            }
            var contact = await _repository.GetByEmailAsync(email);
            if (contact == null) return NotFound();
            return Ok(contact);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> GetContact(int id)
        {
            var contact = await _repository.GetByIdAsync(id);
            if (contact == null) return NotFound();
            return Ok(contact);
        }

        [HttpPost]
        public async Task<ActionResult<Contact>> CreateContact(Contact contact)
        {
            var created = await _repository.AddAsync(contact);
            return CreatedAtAction(nameof(GetContact), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Contact>> UpdateContact(int id, Contact contact)
        {
            if (id != contact.Id) return BadRequest();
            var updated = await _repository.UpdateAsync(contact);
            return Ok(updated);
        }

        [HttpPatch("{id}/phone")]
        public async Task<ActionResult<Contact>> UpdatePhone(int id, [FromBody] string phone)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(phone, @"^\d{3}-\d{3}-\d{4}$"))
            {
                return BadRequest("Phone number must be in the format xxx-xxx-xxxx");
            }
            var updated = await _repository.UpdatePhoneAsync(id, phone);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpPatch("{id}/email")]
        public async Task<ActionResult<Contact>> UpdateEmail(int id, [FromBody] string email)
        {
            var emailValidator = new System.ComponentModel.DataAnnotations.EmailAddressAttribute();
            if (!emailValidator.IsValid(email))
            {
                return BadRequest("Invalid email format");
            }
            var updated = await _repository.UpdateEmailAsync(id, email);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}