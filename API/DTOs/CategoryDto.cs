﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class CategoryDto
    {
        public int Id { get; set; }

        public int AppUserId { get; set; }

        public int? ParentCategoryId { get; set; }

        public int OperationTypeId { get; set; }

        public string Name { get; set; }
    }
}
