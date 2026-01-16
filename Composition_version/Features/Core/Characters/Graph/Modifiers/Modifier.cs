namespace MC.Core.Characters.Graph.Modifiers
{
    public sealed class Modifier
    {
        public ModifierType Type { get; }
        public float Value { get; }
        public string Source { get; }

        public Modifier(ModifierType type, float value, string source = null)
        {
            Type = type;
            Value = value;
            Source = source;
        }
    }
}
