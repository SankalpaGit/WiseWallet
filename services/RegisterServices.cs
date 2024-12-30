using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using WiseWallet.Models;
using WiseWallet.Utils;

namespace WiseWallet.Services
{
    internal class RegisterServices
    {
        private static readonly string FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "WiseWallet", "DataStore.json");

        // Method to ensure file and directory existence
        private static void EnsureFileExists()
        {
            string directory = Path.GetDirectoryName(FilePath)!;
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (!File.Exists(FilePath))
            {
                File.WriteAllText(FilePath, "[]"); // Initialize with empty JSON array
            }
        }

        public static bool RegisterUser(string gmail, string password, string confirmPassword, string currencyType)
        {
            try
            {
                // Check if passwords match
                if (password != confirmPassword)
                {
                    Console.WriteLine("Passwords do not match!");
                    return false;
                }

                // Hash the password
                string hashedPassword = HashPassword.Hash(password);

                // Ensure file exists and load existing users
                EnsureFileExists();
                var users = JsonSerializer.Deserialize<List<UserModel>>(File.ReadAllText(FilePath)) ?? new List<UserModel>();

                // Generate unique UserId
                int newUserId = users.Any() ? users.Max(u => u.UserId) + 1 : 1;

                // Create a new user
                UserModel newUser = new()
                {
                    UserId = newUserId,
                    Gmail = gmail,
                    Password = hashedPassword,
                    CurrencyType = currencyType,
                    Debts = new List<DebtModel>() // Initialize with an empty list of debts
                };

                // Save the user to JSON
                SaveUser(newUser);

                Console.WriteLine("User registered successfully!");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
                return false;
            }
        }


        // Method to save user data
        private static void SaveUser(UserModel user)
        {
            EnsureFileExists();

            // Read existing data
            var users = new List<UserModel>();
            string json = File.ReadAllText(FilePath);
            users = JsonSerializer.Deserialize<List<UserModel>>(json) ?? new List<UserModel>();

            // Add the new user
            users.Add(user);

            // Write updated data back to the file
            string updatedJson = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FilePath, updatedJson);
        }
    }
}
