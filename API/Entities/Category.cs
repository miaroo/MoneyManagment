
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class Category
    {
        public int Id { get; set; }

        public int AppUserId { get; set; }

        public int? ParentCategoryId { get; set; }

        public int OperationTypeId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public OperationType OperationType { get; set; }

        public AppUser AppUser { get; set; }

        public Category ParentCategory { get; set; }

        public IEnumerable<Category> ChildCategories { get; set; }

    }
}