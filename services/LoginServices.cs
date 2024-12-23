using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using WiseWallet.Models;
using WiseWallet.Utils;

namespace WiseWallet.Services
{
    internal class LoginServices
    {
        private static readonly string FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "WiseWallet", "UserModel.json");

        // Method to authenticate user
        public static bool AuthenticateUser(string gmail, string password)
        {
            // Ensure the file exists
            FileCreation.EnsureFileExists();

            // Read the JSON file
            string json = File.ReadAllText(FilePath);
            var users = JsonSerializer.Deserialize<List<UserModel>>(json) ?? new List<UserModel>();

            // Check if user exists
            var user = users.FirstOrDefault(u => u.Gmail == gmail);

            if (user == null)
            {
                Console.WriteLine("User not found!");
                return false;
            }

            // Verify the password
            if (HashPassword.Verify(password, user.Password))
            {
                Console.WriteLine("Login successful!");
                return true;
            }

            Console.WriteLine("Incorrect password!");
            return false;
        }
    }
}
