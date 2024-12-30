using Core.Events;
using UnityEngine;

namespace Move
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterMove : MonoBehaviour
    {
        [SerializeField] float defaultSpeed = 5, defaultRunMult = 2;

        float speed;
        protected bool isRunning;

        new Rigidbody2D rigidbody;

        void MovementSpeedReceived(float movementSpeed)
        {
            speed = movementSpeed >= 0 ? movementSpeed : defaultSpeed;
            StatEventManager.OnMovementSpeedReceived -= MovementSpeedReceived;
        }

        protected void Move(Vector2 movement)
        {
            // Move
            StatEventManager.RequestMovementSpeed(gameObject.GetInstanceID());
            StatEventManager.OnMovementSpeedReceived += MovementSpeedReceived;
            var finalSpeed = speed * (isRunning ? defaultRunMult : 1);
            rigidbody.velocity = finalSpeed * movement;

            // Notify
            var id = gameObject.GetInstanceID();
            if (movement.magnitude > 0)
                EventManager.GOMoved(id, movement, finalSpeed);
            else
                EventManager.GOMoveStopped(id);
        }

        protected void Run(bool running) => isRunning = running;

        void InitRigidbody()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            rigidbody.gravityScale = 0;
            rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            rigidbody.interpolation = RigidbodyInterpolation2D.Interpolate;
        }

        private void Awake()
        {
            InitRigidbody();
        }
    }

}
