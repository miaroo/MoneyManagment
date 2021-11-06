using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class BankAccountParams : PaginationParams
    {
        public int NumberOfRows { get; set; }
        public string CurrentUserName { get; set; }
        public string OrderBy { get; set; }
        public bool Pagination { get; set; }
    }
}
