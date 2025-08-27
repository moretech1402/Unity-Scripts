using System;

namespace Core.Events
{
    public static class GOEventManager
    {
        public static event Action<int> OnDead;
        public static void Dead(int goID) => OnDead?.Invoke(goID);
    }
}
