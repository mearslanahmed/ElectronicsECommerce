using System.Security.Cryptography;
using System.Text;

namespace ElectronicsEcommerce
{
    public static class SecurityHelper
    {
        // Method to hash a password using SHA-256
        public static string HashPassword(string password)
        {
            // Create an instance of SHA256
            using (SHA256 sha256 = SHA256.Create())
            {
                // Compute the hash of the password as a byte array
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert the byte array to a hexadecimal string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                // Return the hashed password
                return builder.ToString();
            }
        }
    }
}