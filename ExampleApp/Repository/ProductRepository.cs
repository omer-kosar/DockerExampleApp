using ExampleApp.Repository;

namespace ExampleApp.Models
{
    public class ProductRepository : IRepository
    {
        private ProductDbContext _context;

        public ProductRepository(ProductDbContext context)
        {
            _context = context;
        }

        public IQueryable<Product> Products => _context.Products;

    }
}