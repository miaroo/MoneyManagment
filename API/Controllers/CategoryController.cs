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

namespace API.Controllers
{
    [Authorize]
    public class CategoryController : BaseApiController
    {
        private readonly ICategoryRepository _category;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public CategoryController(ICategoryRepository categoryRepository,
            IMapper mapper, IUserRepository userRepository)
        {
            _category = categoryRepository;
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
            await _category.AddCategoryAsync(newCategory);
            return Ok(newCategory.Id);
        }
        [HttpPut("categoryId={categoryId}")]
        public async Task<ActionResult<CategoryDto>> UpdateCategory(UpdateCategoryDto updateCategoryDto, int categoryId)
        {
            var userId = User.GetUserId();
            var categoryToUpdate =  await _category.GetCategoryAsync(categoryId);
            if (categoryToUpdate.AppUserId != userId) return BadRequest("This category doesnt belong to this user");
            categoryToUpdate.Name = updateCategoryDto.Name;
            categoryToUpdate.ParentCategoryId = updateCategoryDto.ParentCategoryId;
            await _category.UpdateAsync(categoryToUpdate);

            return Ok(categoryToUpdate.Id);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetUserCategories()
        {
            var userId = User.GetUserId();
            var categories = await _category.GetCategoriesAsync(userId);
            return Ok(categories);
        }

        [HttpDelete("deleteCategoryId={deleteCategoryId}")]
        public async Task<ActionResult> DeleteCategory(int deleteCategoryId)
        {
            var userId = User.GetUserId();
            var categoryToDeleteAndItsChildrens = await _category.
                GetCategoryToDeleteAndChildrenCategoriesAsync(deleteCategoryId);

            var categoryToDeleteParentId = categoryToDeleteAndItsChildrens.ParentCategoryId;

            if (categoryToDeleteAndItsChildrens.AppUserId != userId) return BadRequest("This category doesnt belong to this user");
            await _category.DeleteCategoryAsync(categoryToDeleteAndItsChildrens);

            if (categoryToDeleteAndItsChildrens.ChildCategories.Any() && categoryToDeleteParentId != null)
            {
                foreach (var children in categoryToDeleteAndItsChildrens.ChildCategories)
                {
                    children.ParentCategoryId = categoryToDeleteParentId;
                }
                await _category.UpdateRangeAsync(categoryToDeleteAndItsChildrens.ChildCategories);
            }

            return Ok($"Category number:{deleteCategoryId} has been removed");
        }
    }
}



