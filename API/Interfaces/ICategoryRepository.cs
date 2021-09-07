using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    public interface ICategoryRepository
    {
        Task AddCategory(Category category);
        Task<IEnumerable<Category>> GetCategoriesAsync(int UserId);
        Task Update(Category category);
        Task<Category> GetCategory(int CategoryId);
    }
}
