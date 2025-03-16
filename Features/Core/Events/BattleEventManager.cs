using System;

namespace Core.Events
{
    public static class BattleEventManager
    {
        public static event Action<int, bool> OnAttack;
        public static void Attack(int goID, bool attacking) => OnAttack?.Invoke(goID, attacking);

        public static event Action<int> OnCrushDamaged;
        public static void CrushDamaged(int goID) => OnCrushDamaged?.Invoke(goID);
    }

}
