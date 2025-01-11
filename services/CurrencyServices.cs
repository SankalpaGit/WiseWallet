using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using WiseWallet.Models;

namespace WiseWallet.Services
{
    public static class CurrencyServices
    {
        private static readonly string FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "WiseWallet", "DataStore.json");

        private static readonly Dictionary<string, decimal> ConversionRates = new()
        {
            { "NRS-USD", 0.0075m },
            { "USD-NRS", 133m },
            { "NRS-INR", 0.625m },
            { "INR-NRS", 1.6m },
            { "USD-INR", 83m },
            { "INR-USD", 0.012m }
        };

        public static decimal GetConversionRate(string fromCurrency, string toCurrency)
        {
            string key = $"{fromCurrency}-{toCurrency}";

            if (ConversionRates.ContainsKey(key))
                return ConversionRates[key];

            throw new Exception("Conversion rate not found.");
        }

        public static bool UpdateUserCurrency(int userId, string newCurrency)
        {
            try
            {
                // Ensure file exists
                if (!File.Exists(FilePath))
                {
                    Console.WriteLine("User data file not found.");
                    return false;
                }

                // Load users
                var users = JsonSerializer.Deserialize<List<UserModel>>(File.ReadAllText(FilePath)) ?? new List<UserModel>();

                // Find the user by userId
                var user = users.FirstOrDefault(u => u.UserId == userId);
                if (user == null)
                {
                    Console.WriteLine("User not found.");
                    return false;
                }

                // Convert currency if different
                if (!string.Equals(user.CurrencyType, newCurrency, StringComparison.OrdinalIgnoreCase))
                {
                    decimal conversionRate = GetConversionRate(user.CurrencyType, newCurrency);
                    // Assuming a "Balance" property exists in UserModel
                    
                    user.CurrencyType = newCurrency;
                }

                // Save updated users back to the file
                string updatedJson = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(FilePath, updatedJson);

                Console.WriteLine("User currency updated successfully.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating currency: {ex.Message}");
                return false;
            }
        }
    }
}
