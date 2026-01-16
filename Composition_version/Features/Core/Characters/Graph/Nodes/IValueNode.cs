namespace MC.Core.Characters.Graph.Nodes
{
    public interface IValueNode
    {
        string Id { get; }
        object GetValue();
    }
}
