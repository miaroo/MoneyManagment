using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace API.Entities
{
    public class AppUser :IdentityUser<int>
    {
        public IEnumerable<BankAccount> BankAccounts { get; set; }

        public IEnumerable<Category> Categories { get; set; }

        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}