﻿using System;

namespace WiseWallet.Models
{
    internal class TransactionModel
    {
        public int TransactionId { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; } 
        public string? Tags { get; set; }
        public decimal Amount { get; set; }
        public string? Note { get; set; }
            
        public TransactionModel()
        {
            Type = string.Empty;
            Tags = string.Empty;
            Note = string.Empty;
        }

        public void ConvertAmount(decimal exchangeRate)
        {
            Amount = Math.Round(Amount * exchangeRate, 2); // Convert and round to 2 decimal places
        }
    }
}
