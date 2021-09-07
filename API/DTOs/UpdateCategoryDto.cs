using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class UpdateCategoryDto
    {
        public int? ParentCategoryId { get; set; }

        public string Name { get; set; }
    }
}
