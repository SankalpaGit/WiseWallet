using System;
using WiseWallet.Models;
using WiseWallet.Utils;

namespace WiseWallet.Services
{
    internal class RegisterServices
    {
        public static bool RegisterUser(string gmail, string password, string confirmPassword, string currencyType)
        {
            // Check if passwords match
            if (password != confirmPassword)
            {
                Console.WriteLine("Passwords do not match!");
                return false;
            }

            // Hash the password
            string hashedPassword = HashPassword.Hash(password);

            // Create a new user
            UserModel newUser = new()
            {
                Gmail = gmail,
                Password = hashedPassword,
                CurrencyType = currencyType
            };

            // Save the user to JSON
            FileCreation.SaveUser(newUser);

            Console.WriteLine("User registered successfully!");
            return true;
        }
    }
}
