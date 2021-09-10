using API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        Task<AppUser> GetUserByUsernameAsync(string username);
        Task<AppUser> GetUserByIDAsync(int id);
    }
}
