namespace MC.Core.Unity.Input.Context
{
    public sealed class InputContext
    {
        public readonly string Id;

        public InputContext(string id)
        {
            Id = id;
        }

        public override bool Equals(object obj)
        {
            return obj is InputContext other && Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
