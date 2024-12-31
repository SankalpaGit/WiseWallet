using System;

namespace WiseWallet.Models
{
    internal class TransactionModel
    {
        public int TransactionId { get; set; }
        public DateTime Date { get; set; }
        public string? Tags { get; set; }
        public double Amount { get; set; }
        public string? Note { get; set; }

        public TransactionModel()
        {
            Tags = string.Empty;
            Note = string.Empty;
        }
    }
}
