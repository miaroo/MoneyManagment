using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Interfaces;
using API.Data;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using API.DTOs;
using API.Extensions;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using API.Helpers;

namespace API.Controllers
{
    [Authorize]
    public class CategoryController : BaseApiController
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDto>> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            var userId = User.GetUserId();
            var newCategory = new Category
            {
                AppUserId = userId,
                Name = createCategoryDto.Name,
                OperationTypeId = createCategoryDto.OperationTypeId,
                ParentCategoryId = createCategoryDto.ParentCategoryId
            };
            await _categoryRepository.AddCategoryAsync(newCategory);
            return Ok();
        }
        [HttpPut]
        public async Task<ActionResult<CategoryDto>> UpdateCategory(CategoryDto categoryDto)
        {
            var userId = User.GetUserId();
            if (categoryDto.AppUserId != userId) return BadRequest("This category doesnt belong to this user");
            var category = new Category
            {
                Id = categoryDto.Id,
                AppUserId = userId,
                Name = categoryDto.Name,
                OperationTypeId = categoryDto.OperationTypeId,
                ParentCategoryId = categoryDto.ParentCategoryId
            };
            await _categoryRepository.UpdateAsync(category);

            return Ok();
        }

        [HttpGet("{categoryId}")]
        public async Task<ActionResult<CategoryDto>> GetUserCategory(int categoryId)
        {
            var category =  await _categoryRepository.GetCategoryAsync(categoryId);
            return Ok(category);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetUserPaginatedCategories([FromQuery]UserParams userParams)
        {
            var userId = User.GetUserId();

            if(userParams.Pagination != true)
            {
                var categoriesWithoutPagination = await _categoryRepository.GetCategoriesAsync(userId);
                return Ok(categoriesWithoutPagination);

            }

            var categories = await _categoryRepository.GetPaginatedCategoriesAsync(userParams, userId);
            Response.AddPaginationHeader(categories.CurrentPage, categories.PageSize,
            categories.TotalCount, categories.TotalPages);

            return Ok(categories);
        }

        [HttpDelete("deleteCategoryId={deleteCategoryId}")]
        public async Task<ActionResult> DeleteCategory(int deleteCategoryId)
        {
            var userId = User.GetUserId();
            var category = await _categoryRepository.
                GetCategoryAndChildrenCategoriesAsync(deleteCategoryId);
            if (category == null) return BadRequest("Couldnt find your category");
            var categoryToDeleteParentId = category.ParentCategoryId;

            if (category.AppUserId != userId) return BadRequest("This category doesnt belong to this user");
            await _categoryRepository.DeleteCategoryAsync(category);

            if (category.ChildCategories.Any() && categoryToDeleteParentId != null)
            {
                foreach (var children in category.ChildCategories)
                {
                    children.ParentCategoryId = categoryToDeleteParentId;
                }
                await _categoryRepository.UpdateRangeAsync(category.ChildCategories);
            }

            return Ok();
        }
    }
}



