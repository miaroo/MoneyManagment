using API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class BankAccountsWithThreeOperationsDto
    {
        public int Id { get; set; }

        public int AppUserId { get; set; }

        public string Name { get; set; }

        public DateTime LastActive { get; set; }

        public IEnumerable<OperationDto> Operations { get; set; }
    }
}
