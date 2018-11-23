namespace Spinit.Security.Password
{
    public interface IPasswordCreator
    {
        string CreatePassword(IPasswordParameters parameters);

        string CreatePassword(IPasswordParameters parameters, IPasswordChecker user);
    }
}