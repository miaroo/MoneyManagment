using API.Constant;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            await roleManager.CreateAsync(new AppRole(Roles.Basic.ToString()));
            await roleManager.CreateAsync(new AppRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new AppRole(Roles.Basic.ToString()));
        }
    }
}
