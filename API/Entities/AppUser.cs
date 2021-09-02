using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace API.Entities
{
    public class AppUser
    {
        public IEnumerable<Saldo> Saldos { get; set; }

        public IEnumerable<Category> Categories { get; set; }

        
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        public byte[] PasswordHash { get; set; }
        
        [Required]
        public byte[] PasswordSalt { get; set; }



    }
}