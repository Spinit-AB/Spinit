namespace Spinit.Security.Password
{
    public class PasswordComponents
    {
        public byte[] HashedPassword { get; set; }
        public byte[] Salt { get; set; }
    }
}
