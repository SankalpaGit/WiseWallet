using System.Collections.Generic;

namespace WiseWallet.Models
{
    internal class UserModel
    {
        public int UserId { get; set; }
        public string? Gmail { get; set; }
        public string? Password { get; set; }
        public string? CurrencyType { get; set; }
        public List<DebtModel>? Debts { get; set; } // Nested list of debts
        public List<TransactionModel>? Income { get; set; } 
        public List<TransactionModel>? Expenses { get; set; }

        public UserModel()
        {
            Gmail = string.Empty;
            Password = string.Empty;
            CurrencyType = string.Empty;
            Debts = new List<DebtModel>();
            Income = new List<TransactionModel>();
            Expenses = new List<TransactionModel>();
        }
    }
}
