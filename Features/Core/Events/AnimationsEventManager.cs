using System;

namespace Core.Events
{
    public static class AnimationsEventManager
    {
        public static event Action<int> OnFinishDeadAnimation;
        public static void FinishDeadAnimation(int goID) => OnFinishDeadAnimation?.Invoke(goID);
    }
}