using System.Security.Cryptography;

namespace Spinit.Security.Password
{
    public class PasswordHashService : IPasswordHashService
    {
        private readonly int _iterations;
        private readonly int _hashByteLength;

        public PasswordHashService(int iterations = 64000, int hashByteLength = 18)
        {
            _iterations = iterations;
            _hashByteLength = hashByteLength;
        }

        public byte[] Hash(string password, byte[] salt)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, _iterations))
            {
                return pbkdf2.GetBytes(_hashByteLength);
            }
        }
    }
}