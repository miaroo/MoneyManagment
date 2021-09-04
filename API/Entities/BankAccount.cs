using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace API.Entities
{
    public class BankAccount
    {
        public int Id { get; set; }

        public int AppUserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public AppUser AppUser { get; set; }

        public IEnumerable<Operation> Operations { get; set; }
    }
}