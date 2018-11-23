namespace Spinit.Security.Password
{
    /// <summary>
    /// Parameters used to create a password.
    /// </summary>
    public interface IPasswordParameters
    {
        string PasswordEncoding { get; }
        PasswordEncryptionType PasswordEncryptionType { get; }
        int MinPasswordLength { get; }
        int MaxPasswordLength { get; }
        bool UseUpperCaseCharacters { get; }
        string PasswordCharsUpperCase { get; }
        bool UseLowerCaseCharacters { get; }
        string PasswordCharsLowerCase { get; }
        bool UseNumericCharacters { get; }
        string PasswordCharsNumeric { get; }
        bool UseSpecialCharacters { get; }
        string PasswordCharsSpecial { get; }
    }
}