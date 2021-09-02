
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class Category
    {
        public int ParentCategoryId { get; set; }

        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public string OperationTypeId { get; set; }

        public IEnumerable<Operation> Operation { get; set; }

        public OperationType OperationType { get; set; }

        public AppUser AppUser { get; set; }
    }
}