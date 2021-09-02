using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class Saldo
    {

        public int Id { get; set; }

        public int AppUserId { get; set; }

        [Required]
        public string Name { get; set; }

        public double Money { get; set; }

        public AppUser AppUser { get; set; }

        public IEnumerable<Operation> Operations { get; set; }

    }
}