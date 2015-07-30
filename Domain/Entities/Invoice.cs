using System;

namespace Domain.Entities
{
    public class Invoice : ICondition
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public float Amount { get; set; }

        public int ClientId { get; set; }
    }
}