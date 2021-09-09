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
            var categoryToUpdate =  await _categoryRepository.GetCategoryAsync(categoryId);
            if (categoryToUpdate.AppUserId != userId) return NotFound("This category doesnt belong to this user");
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

        //[HttpGet("user={id}")]
        //public async Task<ActionResult<IEnumerable<CategoryDto>>> GetUserCategories(int id)
        //{
        //    var categoryToDeleteAndItsChildrens = await _categoryRepository.
        //        GetCategoryToDeleteAndChildrenCategories(id);
        //    var x = 5;
        //    var z = 9;
        //    return Ok(categoryToDeleteAndItsChildrens);
        //}

        [HttpDelete("deleteCategoryId={deleteCategoryId}")]
        public async Task<ActionResult> DeleteCategory(int deleteCategoryId)
        {
            var userId = User.GetUserId();
            var categoryToDeleteAndItsChildrens = await _categoryRepository.
                GetCategoryToDeleteAndChildrenCategories(deleteCategoryId);

            var catergoryToDeleteParentId = categoryToDeleteAndItsChildrens.ParentCategoryId;

            if (categoryToDeleteAndItsChildrens.AppUserId != userId) return NotFound("This category doesnt belong to this user");
            await _categoryRepository.DeleteCategoryAsync(categoryToDeleteAndItsChildrens);

            if (categoryToDeleteAndItsChildrens.ChildCategories != null && catergoryToDeleteParentId != null)
            {
                foreach (var children in categoryToDeleteAndItsChildrens.ChildCategories)
                {
                    children.ParentCategoryId = catergoryToDeleteParentId;
                }
                await _categoryRepository.UpdateRangeAsync(categoryToDeleteAndItsChildrens.ChildCategories);
            }

            return Ok($"Category number:{deleteCategoryId} has been removed");
        }
    }
}



