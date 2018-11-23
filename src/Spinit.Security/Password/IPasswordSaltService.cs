namespace Spinit.Security.Password
{
    public interface IPasswordSaltService
    {
        byte[] CreateSalt();
    }
}