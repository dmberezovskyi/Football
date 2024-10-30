using System;
using System.Security.Cryptography;
using Fs.Domain.Services;

namespace Fs.Infrastructure.Security
{
    internal sealed class PasswordHashGenerator : IPasswordHashGenerator
    {
        private const int SaltSize = 32;

        public string Generate(string password, string salt)
        {
            return Compute(password, Convert.FromBase64String(salt));
        }
        public (string Hash, string Salt) Generate(string password)
        {
            var salt = GenerateSalt();
            return (Compute(password, salt), Convert.ToBase64String(salt));
        }

        private static byte[] GenerateSalt()
        {
            var randomNumber = new byte[SaltSize];
            new RNGCryptoServiceProvider().GetBytes(randomNumber);
            return randomNumber;
        }
        public static string Compute(string password, byte[] salt)
        {
            using var hashPassword = new Rfc2898DeriveBytes(password, salt, 2000);
            var hashedPassword = hashPassword.GetBytes(32);
            return Convert.ToBase64String(hashedPassword);
        }
    }
}
