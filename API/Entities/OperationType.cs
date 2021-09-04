using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace API.Entities
{
    public class OperationType
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public IEnumerable<Category> Categories{ get; set; }
    }
}