using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using WiseWallet.Models;

namespace WiseWallet.Utils
{
    internal class FileCreation
    {
        private static readonly string FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "WiseWallet", "UserModel.json");

        // Method to ensure file and directory existence
        public static void EnsureFileExists()
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

        // Method to save user data
        public static void SaveUser(UserModel user)
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
