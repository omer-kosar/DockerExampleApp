using ExampleApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ExampleApp.Repository
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {

        }
       
        public DbSet<Product> Products { get; set; }

    }
}