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
        Task AddCategoryAsync(Category category);
        Task<IEnumerable<Category>> GetCategoriesAsync(int UserId);
        Task UpdateAsync(Category category);
        Task<Category> GetCategoryAsync(int CategoryId);
        Task DeleteCategoryAsync(Category category);
        Task<IEnumerable<Category>> GetChildrenCategories(int? parentCategoryId);
        Task SetChildrenParentIdAsync(IEnumerable<Category> childrenList, int? newParentId);
    }
}
