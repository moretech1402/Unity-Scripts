namespace MC.Core.Characters.Graph.Events
{
    public sealed class EventContext
    {
        public object Source { get; }
        public object Payload { get; }

        public EventContext(object source, object payload = null)
        {
            Source = source;
            Payload = payload;
        }
    }
}
