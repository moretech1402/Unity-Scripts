namespace MC.Core.Definitions
{
    public interface IRuntimeDefinition<out T>
    {
        T Instance { get; }
    }

}