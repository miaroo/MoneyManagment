using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<AppUser> GetUserByIDAsync(int id)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Id == id);
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Username == username);
        }
    }
}
