using System.Runtime.CompilerServices;

namespace Spinit.Security.Password
{
    public class PasswordService : IPasswordService
    {
        private readonly IPasswordHashService _hashService;
        private readonly IPasswordSaltService _saltService;

        public PasswordService(IPasswordHashService hashService, IPasswordSaltService saltService)
        {
            _hashService = hashService;
            _saltService = saltService;
        }

        public PasswordComponents Create(string requestedPassword)
        {
            var salt = _saltService.CreateSalt();
            return new PasswordComponents
            {
                HashedPassword = _hashService.Hash(requestedPassword, salt),
                Salt = salt
            };
        }

        public bool VerifyPassword(byte[] expectedPasswordHash, string inputPassword, byte[] salt)
        {
            var hashedPassword = _hashService.Hash(inputPassword, salt);

            return SlowEquals(expectedPasswordHash, hashedPassword);
        }

        [MethodImpl(MethodImplOptions.NoOptimization)]
        protected bool SlowEquals(byte[] first, byte[] second)
        {
            var diff = first.Length ^ second.Length;
            for (var i = 0; i < first.Length && i < second.Length; i++)
            {
                diff |= first[i] ^ second[i];
            }

            return diff == 0;
        }
    }
}
