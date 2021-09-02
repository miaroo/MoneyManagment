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
        
        [Required]
        public double Amount { get; set; }

        [Required]
        public DateTime Date { get; set; } = DateTime.Now;

        [Required]
        [MaxLength(100)]
        public string OperationName { get; set; }

        public int CategoryId { get; set; }

        public int SaldoId { get; set; }

        public Saldo Saldo { get; set; }

        public Category Category  { get; set; }

    }
}