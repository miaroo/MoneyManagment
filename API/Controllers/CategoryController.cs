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
        private readonly DataContext _context;

        private readonly ICategoryRepository _categoryRepository;

        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public CategoryController(DataContext context, ICategoryRepository categoryRepository,
            IMapper mapper, IUserRepository userRepository)
        {
            _context = context;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<ActionResult<Category>> CreateCategory(Category category)
        {
            var userd = User.FindFirstValue(ClaimTypes.Name);
            //var name = await _context.Users.FirstOrDefault(u => u.Username == userd);
            var name = await _userRepository.GetUserByUsernameAsync(userd);

            var categoryprop = new Category
            {
                AppUserId = name.Id,
                Name = category.Name,
                OperationTypeId = category.OperationTypeId,
                ParentCategoryId = category.ParentCategoryId
            };
            _categoryRepository.AddCategory(categoryprop);
            return Ok(categoryprop.Id);
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



