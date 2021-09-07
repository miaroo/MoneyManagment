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
        public async Task<ActionResult<Category>> CreateCategory(Category category)
        {
            var userId = User.GetUserId();

            var newCategory = new Category
            {
                AppUserId = userId,
                Name = category.Name,
                OperationTypeId = category.OperationTypeId,
                ParentCategoryId = category.ParentCategoryId
            };
            await _categoryRepository.AddCategory(newCategory);
            return Ok(newCategory.Id);
        }
        [HttpPut("categoryId/{categoryId}")]
        public async Task<ActionResult<Category>> UpdateCategory(UpdateCategoryDto updateCategoryDto, int categoryId)
        {
            var userId = User.GetUserId();
            var categoryToUpdate =  await _categoryRepository.GetCategory(categoryId);
            if (categoryToUpdate.AppUserId != userId) return NotFound("This category doesnt belong to this user");
            categoryToUpdate.Name = updateCategoryDto.Name;
            categoryToUpdate.ParentCategoryId = updateCategoryDto.ParentCategoryId;

            await _categoryRepository.Update(categoryToUpdate);
            return Ok(categoryToUpdate.Name);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetUserCategories()
        {
            var user = User.GetUserId();
            var categories = await _categoryRepository.GetCategoriesAsync(user);
            return Ok(categories);
        }
    }
}



