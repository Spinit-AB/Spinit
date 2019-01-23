namespace Spinit.IO
{
    public abstract class Wrapper<T> : IWrapper<T>
    {
        protected readonly T Wrapped;

        protected Wrapper(T wrapped)
        {
            Wrapped = wrapped;
        }

        public T UnWrap()
        {
            return Wrapped;
        }
    }
}