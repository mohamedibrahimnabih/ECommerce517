using ECommerce517.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ECommerce517.Repositories
{
    public class CategoryRepository
    {
        private ApplicationDbContext _context = new();

        // CRUD
        public async Task CreateAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
        }

        public void Update(Category category)
        {
            _context.Categories.Update(category);
        }

        public void Delete(Category category)
        {
            _context.Categories.Remove(category);
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<Category>> GetAsync(Expression<Func<Category, bool>>? expression = null)
        {
            var categories = _context.Categories.AsQueryable();

            if(expression is not null)
            {
                categories = categories.Where(expression);
            }

            return await categories.ToListAsync();
        }

        public async Task<Category?> GetOneAsync(Expression<Func<Category, bool>> expression)
        {
            return (await GetAsync(expression)).FirstOrDefault();
        }
    }
}
