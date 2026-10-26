using Microsoft.EntityFrameworkCore;
using ContactDemo.Models; // Replace with your actual namespace

namespace ContactDemo.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Contact> Contacts { get; set; }
}