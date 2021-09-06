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

        public Task<AppUser> GetUserByIDAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            //return await _context.Users.SingleOrDefaultAsync(u => u.Username == username);
            return  _context.Users.FirstOrDefault(u => u.Username == username);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
            _context.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
