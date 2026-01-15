using ContactsApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactsApp.Data
{
    public interface IContactRepository
    {
        Task<IEnumerable<Contact>> GetAllAsync();
        Task<Contact?> GetByIdAsync(int id);
        Task<Contact?> GetByPhoneAsync(string phone);
        Task<Contact?> GetByEmailAsync(string email);
        Task<Contact?> UpdatePhoneAsync(int id, string phone);
        Task<Contact?> UpdateEmailAsync(int id, string email);
        Task<Contact> AddAsync(Contact contact);
        Task<Contact> UpdateAsync(Contact contact);
        Task<bool> DeleteAsync(int id);
    }
}