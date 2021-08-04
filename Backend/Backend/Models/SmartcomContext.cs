
using Microsoft.EntityFrameworkCore;

namespace Backend.Models
{
    public class SmartcomContext : DbContext
    {
        public DbSet<Role> Roles { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Status> Statuses { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderElement> OrderElements { get; set; }

        public DbSet<Product> Products { get; set; }
 
        public SmartcomContext(DbContextOptions<SmartcomContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
