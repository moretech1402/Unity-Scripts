namespace MC.Core.Characters.Graph.Runtime
{
    public interface ITemporal
    {
        bool Tick(GraphContext context);
    }
}
