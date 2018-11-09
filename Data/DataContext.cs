using LoginRegistration.Models;
using Microsoft.EntityFrameworkCore;

namespace LoginRegistration.Data
{
    public class DataContext : DbContext
    {
        public DataContext (DbContextOptions<DataContext> options) : base (options) {}

        public DbSet<User> Users { get; set; }
    }
}