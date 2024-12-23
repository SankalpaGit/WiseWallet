using System.Security.Cryptography;
using System.Text;

namespace WiseWallet.Utils
{
    internal static class HashPassword
    {
        // Method to hash a password
        public static string Hash(string password)
        {
            using var sha256 = SHA256.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            byte[] hashBytes = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hashBytes);
        }

        // (Optional) Method to verify a hashed password
        public static bool Verify(string password, string hashedPassword)
        {
            string hashedInput = Hash(password);
            return hashedInput == hashedPassword;
        }
    }
}
