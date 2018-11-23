using System;
using System.Security.Cryptography;

namespace Spinit.Security.Password
{
    public class PasswordSaltService : IPasswordSaltService
    {
        private readonly int _minSaltSize;
        private readonly int _maxSaltSize;
        private readonly Random _random;
        private readonly RandomNumberGenerator _rng;

        public PasswordSaltService(int minSaltSize = 16, int maxSaltSize = 32)
        {
            _minSaltSize = minSaltSize;
            _maxSaltSize = maxSaltSize;
            _random = new Random();
            _rng = RandomNumberGenerator.Create();
        }

        public byte[] CreateSalt()
        {
            var saltSize = _random.Next(_minSaltSize, _maxSaltSize);
            var saltBytes = new byte[saltSize];
            
            _rng.GetNonZeroBytes(saltBytes);
            return saltBytes;
        }
    }
}