namespace MC.Core.Unity.Cameras
{
    public sealed class CameraContext
    {
        public readonly string Id;

        public CameraContext(string id)
        {
            Id = id;
        }

        public override bool Equals(object obj)
        {
            if (obj is not CameraContext other)
                return false;

            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id != null ? Id.GetHashCode() : 0;
        }
    }

    public struct CameraContextChangedEvent
    {
        public CameraContext Context;
    }
}