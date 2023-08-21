using Microsoft.EntityFrameworkCore;
using Test3.Models;

namespace Test3.Data
{
    public class ApplicationDbContext : DbContext
    {
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        

        public DbSet<BookStore> Books { get; set; }
        
        public DbSet<Register> Registers { get; set; }
        public DbSet<Login> Logins { get; set; }
    }
}
