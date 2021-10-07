using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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

        public async Task DeleteCategoryAsync(Category category)
        {
            _context.Remove(category);
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

        public async Task<Category> GetCategoryAndChildrenCategoriesAsync(int categoryId)
        {
            return await _context.Categories
                    .Include(c => c.ChildCategories)
                    .SingleOrDefaultAsync(x => x.Id == categoryId);
        }

        public async Task UpdateRangeAsync(IEnumerable<Category> childrenList)
        {
            _context.Categories.UpdateRange(childrenList);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task<PagedList<CategoryDto>> GetPaginatedCategoriesAsync(UserParams userParams, int appUserId)
        {
            var query = _context.Categories
                .Where(b => b.AppUserId == appUserId);

            query = userParams.OrderBy switch
            {
                "created" => query.OrderByDescending(u => u.Id),
                _ => query.OrderBy(u => u.Name)
            };

            return await PagedList<CategoryDto>.CreateAsync(query.ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
            , userParams.PageNumber, userParams.PageSize);
        }
    }
}
