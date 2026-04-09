using Microsoft.EntityFrameworkCore;
namespace ContactApp.Models;

public class ContactContext : DbContext
{
    public ContactContext(DbContextOptions<ContactContext> options) : base(options) { }

    public DbSet<Contact> Contacts { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contact>().HasData(
            new Contact
            {
                ContactId = 1,
                Name = "John",
                PhoneNumber = "1123456789",
                Address = "Somewhere in the world"
            },
            new Contact
            {
                ContactId = 2,
                Name = "Jane",
                PhoneNumber = "9908872432",
                Address = "Texas"
            },
            new Contact
            {
                ContactId = 3,
                Name = "Jake",
                PhoneNumber = "4523366412",
                Address = "Davenport Iowa",
                Note = "Works at State Farm"
            },
            new Contact
            {
                ContactId = 4,
                Name = "Dua",
                PhoneNumber = "3334441234",
                Address = "California",
                Note = "Is a Singer"
            }
            );

    }
}