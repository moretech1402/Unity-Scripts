using System;

namespace Core.Events
{
    public static class StatsEventManager
    {
        public static event Action<Guid, float> OnHealthChanged;
        public static void HealthChanged(Guid ID, float health) => OnHealthChanged?.Invoke(ID, health);

        public static event Action<Guid> OnDead;
        public static void Dead(Guid ID) => OnDead?.Invoke(ID);
    }
}