using System.Security.Cryptography;

namespace LoginApp.API.Infrastructure.Services
{
    public static class SecurityHelper
    {
        public static byte[] GenerateRandomKey(int length)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var key = new byte[length];
                rng.GetBytes(key);
                return key;
            }
        }
    }
}