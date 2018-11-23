namespace Spinit.Security.Password
{
    public interface IPasswordService
    {
        PasswordComponents Create(string requestedPassword);
        bool VerifyPassword(byte[] expectedPasswordHash, string inputPassword, byte[] salt);
    }
}
