namespace Core.Interfaces
{
    public interface IConfigurable<T>
    {
        void Configure(T sheet);
    }

}
