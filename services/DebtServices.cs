using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using WiseWallet.Models;
using WiseWallet.Utils;

namespace WiseWallet.Services
{
    internal static class DebtServices
    {
        private static readonly string FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "WiseWallet", "DataStore.json");

        // Ensure the file exists
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

        // Add a debt for a specific user
        public static bool AddDebt(int userId, DebtModel debt)
        {
            try
            {
                FileCreation.EnsureFileExists();

                // Read existing data
                string json = File.ReadAllText(FileCreation.FilePath);
                var users = JsonSerializer.Deserialize<List<UserModel>>(json) ?? new List<UserModel>();

                // Find the user by UserId
                var user = users.FirstOrDefault(u => u.UserId == userId);
                if (user == null)
                {
                    Console.WriteLine("User not found!");
                    return false;
                }

                // Initialize the Debts list if null
                user.Debts ??= new List<DebtModel>();

                // Add the new debt
                debt.DebtId = user.Debts.Count + 1; // Auto-generate DebtId
                user.Debts.Add(debt);

                // Write updated data back to the file
                string updatedJson = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(FileCreation.FilePath, updatedJson);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding debt: {ex.Message}");
                return false;
            }
        }

        public static void UpdateDebt(int userId, DebtModel updatedDebt)
        {
            FileCreation.EnsureFileExists();

            // Read JSON data
            string json = File.ReadAllText(FilePath);
            var users = JsonSerializer.Deserialize<List<UserModel>>(json) ?? new List<UserModel>();

            // Find the user
            var user = users.FirstOrDefault(u => u.UserId == userId);
            if (user != null)
            {
                // Update the debt
                var debt = user.Debts?.FirstOrDefault(d => d.DebtId == updatedDebt.DebtId);
                if (debt != null)
                {
                    debt.Status = updatedDebt.Status;
                }

                // Save updated data
                string updatedJson = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(FilePath, updatedJson);
            }
        }

        public static List<DebtModel> GetUserDebts(int userId)
        {
            FileCreation.EnsureFileExists();

            // Read JSON data
            string json = File.ReadAllText(FilePath);
            var users = JsonSerializer.Deserialize<List<UserModel>>(json) ?? new List<UserModel>();

            // Find the user and return their debts
            var user = users.FirstOrDefault(u => u.UserId == userId);
            return user?.Debts ?? new List<DebtModel>();
        }

    }
}
