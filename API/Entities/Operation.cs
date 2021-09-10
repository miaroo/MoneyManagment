using System;
using System.ComponentModel.DataAnnotations;
namespace API.Entities
{
    public class Operation
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Description { get; set; }
        
        public decimal Amount { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public int? CategoryId { get; set; }

        public int BankAccountId { get; set; }

        public BankAccount BankAccount { get; set; }

        public Category Category  { get; set; }
    }
}