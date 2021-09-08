using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CategoryRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddCategoryAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int CategoryId)
        {
            _context.Remove(await _context.Categories.SingleOrDefaultAsync(d => d.Id == CategoryId));
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync(int AppUserId)
        {
            return await _context.Categories
                .Where(b => b.AppUserId == AppUserId)
                .ToListAsync();
        }

        public async Task<Category> GetCategoryAsync(int CategoryId)
        {
            return await _context.Categories
                .SingleOrDefaultAsync(c => c.Id == CategoryId);
        }

        public async Task UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();

        }
    }
}
