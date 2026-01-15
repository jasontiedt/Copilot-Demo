using ContactsApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactsApp.Data
{
    public class ContactRepository : IContactRepository
    {
        private readonly ContactsContext _context;

        public ContactRepository(ContactsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Contact>> GetAllAsync()
        {
            return await _context.Contacts.ToListAsync();
        }

        public async Task<Contact?> GetByIdAsync(int id)
        {
            return await _context.Contacts.FindAsync(id);
        }

        public async Task<Contact?> GetByPhoneAsync(string phone)
        {
            return await _context.Contacts.FirstOrDefaultAsync(c => c.Phone == phone);
        }

        public async Task<Contact> AddAsync(Contact contact)
        {
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
            return contact;
        }

        public async Task<Contact?> GetByEmailAsync(string email)
        {
            return await _context.Contacts.FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<Contact?> UpdatePhoneAsync(int id, string phone)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null) return null;
            contact.Phone = phone;
            await _context.SaveChangesAsync();
            return contact;
        }

        public async Task<Contact?> UpdateEmailAsync(int id, string email)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null) return null;
            contact.Email = email;
            await _context.SaveChangesAsync();
            return contact;
        }

        public async Task<Contact> UpdateAsync(Contact contact)
        {
            _context.Contacts.Update(contact);
            await _context.SaveChangesAsync();
            return contact;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null) return false;
            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}