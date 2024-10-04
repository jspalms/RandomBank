using System;

namespace Accounts.Domain.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public TransactionType Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Reference { get; set; }
        public decimal Amount { get; set; }
    }
}
