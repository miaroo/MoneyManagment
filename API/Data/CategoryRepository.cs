using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using API.Data;


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

        public void AddCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();

        }

        public void DeleteCategory(Category category)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> GetAll(int Id)
        {
            return _context.Categories.Where(x => x.AppUserId == Id).ToList();
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync(int Id)
        {
            return await _context.Categories
                .Where(b => b.AppUserId == Id)
                .ToListAsync();
        }

        public CategoryDto GetCategory(int UserId, string Name)
        {
            throw new NotImplementedException();
        }

        public void Update(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
