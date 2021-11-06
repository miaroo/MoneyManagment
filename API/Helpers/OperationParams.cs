using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class OperationParams : PaginationParams
    {
        public int bankAccountId { get; set; }
        public string OrderBy { get; set; }
        public bool Pagination { get; set; }
    
    }
}
