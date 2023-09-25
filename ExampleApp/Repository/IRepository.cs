using ExampleApp.Models;

namespace ExampleApp.Repository
{
    public interface IRepository
    {
        IQueryable<Product> Products { get; }
    }
}