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
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public CategoryController(ICategoryRepository categoryRepository,
            IMapper mapper, IUserRepository userRepository)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDto>> CreateCategory(CategoryDto categoryDto)
        {
            var userId = User.GetUserId();

            var newCategory = new Category
            {
                AppUserId = userId,
                Name = categoryDto.Name,
                OperationTypeId = categoryDto.OperationTypeId,
                ParentCategoryId = categoryDto.ParentCategoryId
            };
            await _categoryRepository.AddCategoryAsync(newCategory);
            return Ok(newCategory.Id);
        }
        [HttpPut("categoryId={categoryId}")]
        public async Task<ActionResult<CategoryDto>> UpdateCategory(UpdateCategoryDto updateCategoryDto, int categoryId)
        {
            var userId = User.GetUserId();
            var categoryToUpdate = await _categoryRepository.GetCategoryAsync(categoryId);
            if (categoryToUpdate.AppUserId != userId) return BadRequest("This category doesnt belong to this user");
            categoryToUpdate.Name = updateCategoryDto.Name;
            categoryToUpdate.ParentCategoryId = updateCategoryDto.ParentCategoryId;
            await _categoryRepository.UpdateAsync(categoryToUpdate);

            return Ok(categoryToUpdate.Id);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetUserCategories()
        {
            var userId = User.GetUserId();
            var categories = await _categoryRepository.GetCategoriesAsync(userId);
            return Ok(categories);
        }

        [HttpGet("{categoryId}")]
        public async Task<ActionResult<CategoryDto>> GetUserCategory(int categoryId)
        {
            var category =  await _categoryRepository.GetCategoryAsync(categoryId);
            return Ok(category);
        }

        [HttpGet("pagination")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetUserPaginatedCategories([FromQuery]UserParams userParams)
        {
            //var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());
            userParams.CurrentUserName = User.GetUsername();
            
            var categories = await _categoryRepository.GetPaginatedCategories(userParams, User.GetUserId());
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

            return Ok($"Category number:{deleteCategoryId} has been removed");
        }
    }
}



