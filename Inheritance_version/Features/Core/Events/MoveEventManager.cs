using System;

namespace Core.Events
{
    /// <summary> Enumeration that represents the possible states of movement of a gameobject. </summary>
    public enum MovementState
    {
        /// <summary>The gameobject is stopped.</summary>
        Stopped = 0,

        /// <summary>The gameobject is walking.</summary>
        Walking = 1,

        /// <summary>The gameobject is running.</summary>
        Running = 2
    }

    public class MoveEventManager : Singleton<MoveEventManager>
    {

        // Speed = 0 -> stop; 1 -> walking; 2 -> running
        /// <summary>Event that notifies when a gameobject changes its state of movement.</summary>
        public static event Action<int, MovementState> OnComplexMove;

        /// <summary> Notify a change in the state of movement of a specific gameobject. </summary>
        /// <param name="goID">The unique identifier of the gameobject that changes its state of movement.</param>
        /// <param name="movementState">The new state of movement (detained, walking or running).</param>
        public static void ComplexMove(int goID, MovementState movementState) => OnComplexMove?.Invoke(goID, movementState);

        public static event Action<int, bool> OnIsGrounded;
        public static void IsGrounded(int goID, bool isGrounded) => OnIsGrounded?.Invoke(goID, isGrounded);
    }

}
