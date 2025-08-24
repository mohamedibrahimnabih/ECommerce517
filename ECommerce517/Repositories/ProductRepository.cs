using ECommerce517.Repositories.IRepositories;

namespace ECommerce517.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _context = new();

        public async Task AddRangeAsync(List<Product> products)
        {
            await _context.Products.AddRangeAsync(products);
        }

    }
}
