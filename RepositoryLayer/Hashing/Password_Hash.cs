using NLog;
using System.Security.Cryptography;

namespace RepositoryLayer.Hashing
{
    public class Password_Hash
    {
        private readonly static ILogger logger = NLog.LogManager.GetCurrentClassLogger();
        private const int SaltSize = 16;
        private const int HashSize = 20;
        private const int Iterations = 10000;

        public string HashPassword(string password)
        {
            if (password == null) {
                logger.Error("Password is null");
                return null;
            }
            logger.Info("Hashing password");
            byte[] salt = new byte[SaltSize];
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[SaltSize]);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations);
            byte[] hash = pbkdf2.GetBytes(HashSize);

            byte[] hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

            logger.Info("Password hashed successfully");
            return Convert.ToBase64String(hashBytes);
        }

        public bool VerifyPassword(string password, string storedHash)
        {
            if (password == null || storedHash == null)
            {
                logger.Error("Password or storedHash is null");
                return false;
            }
            logger.Info("Verifying password");
            byte[] hashBytes = Convert.FromBase64String(storedHash);
            byte[] salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations);
            byte[] hash = pbkdf2.GetBytes(HashSize);

            logger.Info("Password verified successfully");
            return CompareHashes(hash, hashBytes);
        }

        private bool CompareHashes(byte[] hash, byte[] hashBytes)
        {
            logger.Info("Comparing hashes");
            for (int i = 0; i < HashSize; i++)
            {
                if (hashBytes[i + SaltSize] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
