﻿using System;
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
        private static readonly string FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "WiseWallet", "DataStore.json");

        // Track the currently logged-in user
        private static UserModel? LoggedInUser = null;

        // Method to authenticate user
        public static bool AuthenticateUser(string gmail, string password)
        {
            // Ensure the file exists
            FileCreation.EnsureFileExists();

            // Read the JSON file
            string json = File.ReadAllText(FilePath);
            var users = JsonSerializer.Deserialize<List<UserModel>>(json) ?? new List<UserModel>();

            // Find the user by Gmail
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

                // Set the logged-in user
                LoggedInUser = user;
                return true;
            }

            Console.WriteLine("Incorrect password!");
            return false;
        }

        // Method to get the logged-in user
        public static UserModel? GetLoggedInUser()
        {
            return LoggedInUser;
        }

        // Method to log out the current user
        public static void Logout()
        {
            LoggedInUser = null;
            Console.WriteLine("User logged out successfully!");
        }
    }
}
