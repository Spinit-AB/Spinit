namespace Spinit.IO
{
    public interface IWrapper<out T>
    {
        T UnWrap();
    }
}