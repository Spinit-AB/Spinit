namespace Spinit.Security.Password
{
    public interface IPasswordHashService
    {
        byte[] Hash(string password, byte[] salt);
    }
}