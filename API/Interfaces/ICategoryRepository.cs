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
        Task<int> AddCategoryAsync(Category category);
        Task<IEnumerable<Category>> GetCategoriesAsync(int userId);
        Task UpdateAsync(Category category);
        Task<Category> GetCategoryAsync(int categoryId);
        Task DeleteCategoryAsync(Category category);
        Task<Category> GetCategoryAndChildrenCategoriesAsync(int categoryToDeleteId);

        Task<PagedList<CategoryDto>> GetPaginatedCategoriesAsync(UserParams userParams, int appUserId);
        Task UpdateRangeAsync(IEnumerable<Category> childrenList);
    }
}
