using System;
using UnityEngine;

namespace Core.Events
{
    public static class StatEventManager
    {
        public static event Action<int> OnRequestMovementSpeed;
        public static void RequestMovementSpeed(int goID) => OnRequestMovementSpeed?.Invoke(goID);

        public static event Action<float> OnMovementSpeedReceived;
        public static void MovementSpeedReceived(float value) => OnMovementSpeedReceived?.Invoke(value);
    }

}
