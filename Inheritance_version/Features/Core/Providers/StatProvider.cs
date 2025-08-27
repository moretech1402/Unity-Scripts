using Core.Events;
using Stats;

namespace Core.Providers
{
    public class StatProvider : Singleton<StatProvider>
    {
        void RequestMovementSpeed(int goID)
        {
            var statComponents = FindObjectsOfType<AStatsHandler>();
            float value = -1;
            foreach (var statComp in statComponents)
            {
                if (statComp.gameObject.GetInstanceID().Equals(goID))
                {
                    value = statComp.GetCurrent(StatKeys.MovementSpeed);
                    break;
                }
            }
            StatEventManager.MovementSpeedReceived(value);
        }

        private new void Awake()
        {
            StatEventManager.OnRequestMovementSpeed += RequestMovementSpeed;
        }

        private void OnDestroy()
        {
            StatEventManager.OnRequestMovementSpeed -= RequestMovementSpeed;
        }
    }

}
