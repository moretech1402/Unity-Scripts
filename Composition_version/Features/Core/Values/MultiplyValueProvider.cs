namespace MC.Core.Values
{
    public sealed class MultiplyValueProvider : IValueProvider<int>
    {
        private readonly IValueProvider<int> _source;
        private readonly int _multiplier;

        public MultiplyValueProvider(
            IValueProvider<int> source,
            int multiplier)
        {
            _source = source;
            _multiplier = multiplier;
        }

        public int Get()
        {
            return _source.Get() * _multiplier;
        }
    }

}
