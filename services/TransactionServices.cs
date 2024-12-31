using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using WiseWallet.Models;

namespace WiseWallet.Services
{
    internal class TransactionServices
    {
        private static readonly string FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "WiseWallet", "DataStore.json");

        private static void EnsureFileExists()
        {
            string directory = Path.GetDirectoryName(FilePath)!;
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (!File.Exists(FilePath))
            {
                File.WriteAllText(FilePath, "[]");
            }
        }

        public static bool AddIncome(int userId, TransactionModel income)
        {
            return AddTransaction(userId, income, "Income");
        }

        public static bool AddExpense(int userId, TransactionModel expense)
        {
            return AddTransaction(userId, expense, "Expenses");
        }

        private static bool AddTransaction(int userId, TransactionModel transaction, string type)
        {
            EnsureFileExists();

            string json = File.ReadAllText(FilePath);
            var users = JsonSerializer.Deserialize<List<UserModel>>(json) ?? new List<UserModel>();

            var user = users.FirstOrDefault(u => u.UserId == userId);

            if (user != null)
            {
                if (type == "Income")
                {
                    transaction.TransactionId = user.Income?.Count + 1 ?? 1;
                    user.Income ??= new List<TransactionModel>();
                    user.Income.Add(transaction);
                }
                else if (type == "Expenses")
                {
                    transaction.TransactionId = user.Expenses?.Count + 1 ?? 1;
                    user.Expenses ??= new List<TransactionModel>();
                    user.Expenses.Add(transaction);
                }

                string updatedJson = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(FilePath, updatedJson);
                return true;
            }

            return false;
        }
    }
}
