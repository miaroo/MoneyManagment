using System;

namespace API.Entities
{
    public class Operation
    {
        public double Amount { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        public int Id { get; set; }

        public string OperationName { get; set; }

        public int CategoryId { get; set; }

        public string Description { get; set; }

        public int SaldoId { get; set; }

        public Saldo Saldo { get; set; }

        public Category Category  { get; set; }

    }
}