namespace Spinit.Security.Password
{
    public interface IPasswordChecker
    {
        bool CheckIfPasswordExist(string password);
    }
}