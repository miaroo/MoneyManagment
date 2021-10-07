using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class CreateOperationDto
    {
        public string Description { get; set; }

        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        public string Name { get; set; }

        public int? CategoryId { get; set; }

        public int BankAccountId { get; set; }
    }
}
