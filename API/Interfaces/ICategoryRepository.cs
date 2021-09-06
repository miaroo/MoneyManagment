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
        void AddCategory(Category category);

        void DeleteCategory(Category category);

        CategoryDto GetCategory(int UserId, string Name);

        public IEnumerable<Category> GetAll(int Id);

        Task<IEnumerable<Category>> GetCategoriesAsync(int UserId);

        void Update(Category category);
    }
}
