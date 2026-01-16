namespace MC.Core.Values
{
    public sealed class ConstantValueProvider<T> : IValueProvider<T>
    {
        private readonly T _value;

        public ConstantValueProvider(T value)
        {
            _value = value;
        }

        public T Get() => _value;
    }

}
