namespace MC.Core.Unity.Movement {
    public interface INavigator {
        void Navigate(UnityEngine.Vector3 direction);
        void Stop();
        bool IsMoving { get; }
    }
}
