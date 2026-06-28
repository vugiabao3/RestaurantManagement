using System.Security.Cryptography;
using System.Text;
using RestaurantManagement.Application.Interfaces;

namespace RestaurantManagement.Infrastructure.Authentication
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int Iterations = 100_000;
        private const int SaltSize = 16;
        private const int KeySize = 32;
        private const string Prefix = "PBKDF2";

        public string Hash(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(SaltSize);
            var key = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithmName.SHA256, KeySize);
            return $"{Prefix}${Iterations}${Convert.ToBase64String(salt)}${Convert.ToBase64String(key)}";
        }

        public bool Verify(string password, string storedHash)
        {
            if (!storedHash.StartsWith(Prefix + "$", StringComparison.Ordinal))
            {
                var supplied = Encoding.UTF8.GetBytes(password);
                var stored = Encoding.UTF8.GetBytes(storedHash);
                return supplied.Length == stored.Length && CryptographicOperations.FixedTimeEquals(supplied, stored);
            }

            var parts = storedHash.Split('$');
            if (parts.Length != 4 || !int.TryParse(parts[1], out var iterations)) return false;

            try
            {
                var salt = Convert.FromBase64String(parts[2]);
                var expected = Convert.FromBase64String(parts[3]);
                var actual = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, HashAlgorithmName.SHA256, expected.Length);
                return CryptographicOperations.FixedTimeEquals(actual, expected);
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public bool NeedsRehash(string storedHash) => !storedHash.StartsWith(Prefix + "$", StringComparison.Ordinal);
    }
}
