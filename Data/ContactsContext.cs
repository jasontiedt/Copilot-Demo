using ContactsApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactsApp.Data
{
    public class ContactsContext : DbContext
    {
        public ContactsContext(DbContextOptions<ContactsContext> options) : base(options) { }

        public DbSet<Contact> Contacts { get; set; }
    }
}