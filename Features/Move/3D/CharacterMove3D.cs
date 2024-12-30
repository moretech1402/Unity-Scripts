using System;
using Core.Events;
using UnityEngine;

namespace Move
{
    public enum MoveStatKeys
    {
        MovementSpeed, JumpForce
    }

    [RequireComponent(typeof(CharacterController))]
    public class CharacterMove3D : MonoBehaviour, ICharacterMove3D
    {
        [SerializeField] float defaultSpeed = 5, defaultRunMult = 2, defaultJumpForce = 5f, slideSpeed = 7, slideDownSpeed = 10;
        [SerializeField] GameObject statsProvider;

        float speed, runMult = 1;
        Vector3 velocity, hitNormal;

        const float GRAVITY_VALUE = -9.81f;

        CharacterController controller;

        int StatsID => statsProvider.GetInstanceID();

        float ValueOrDefault(float value, float defaultValue) => value >= 0 ? value : defaultValue;

        private void RequestStatValue(MoveStatKeys key, Action<float> valueReceivedCallback, float defaultValue)
        {
            if (statsProvider == null)
            {
                valueReceivedCallback(defaultValue);
                return;
            }

            switch (key)
            {
                case MoveStatKeys.MovementSpeed:
                    StatsEventManager.OnMovementSpeedReceived += valueReceivedCallback;
                    StatsEventManager.RequestMovementSpeed(StatsID);
                    return;
                case MoveStatKeys.JumpForce:
                    StatsEventManager.OnJumpForceReceived += valueReceivedCallback;
                    StatsEventManager.RequestJumpForce(StatsID);
                    return;
                default:
                    valueReceivedCallback(defaultValue);
                    return;
            }
        }

        private void JumpForceReceived(float value)
        {
            StatsEventManager.OnJumpForceReceived -= JumpForceReceived;
            velocity.y = ValueOrDefault(value, defaultJumpForce);
        }

        public void Jump()
        {
            if (controller.isGrounded)
                RequestStatValue(MoveStatKeys.JumpForce, JumpForceReceived, defaultJumpForce);
        }

        public void Run(bool running) => runMult = running ? defaultRunMult : 1;

        private void MovementSpeedReceived(float value)
        {
            StatsEventManager.OnMovementSpeedReceived -= MovementSpeedReceived;
            speed = ValueOrDefault(value, defaultSpeed);
        }

        public void Move(Vector3 movement)
        {
            // Calculate Speed
            if (movement == Vector3.zero) speed = 0;
            else RequestStatValue(MoveStatKeys.MovementSpeed, MovementSpeedReceived, defaultSpeed);

            // To Avoid speed increase when diagonal move
            var normalizedMovement = Vector3.ClampMagnitude(movement, 1);

            // Rotation to target
            transform.LookAt(transform.position + normalizedMovement);

            var finalMove = speed * runMult * normalizedMovement;
            velocity = new(finalMove.x, velocity.y, finalMove.z);
        }

        private void HandleGravity()
        {
            var yAceleration = GRAVITY_VALUE * Time.deltaTime;
            velocity.y += yAceleration;

            if (controller.isGrounded && velocity.y < 0) velocity.y = -1 * Mathf.Epsilon;
        }

        void SlopeDown(){
            var isOnSlope = Vector3.Angle(Vector3.up, hitNormal) >= controller.slopeLimit;
            if(isOnSlope){
                float AddSlideSpeed(float current, float axis) => current + (1f - hitNormal.y) * axis * slideSpeed;
                velocity = new(AddSlideSpeed(velocity.x, hitNormal.x), velocity.y -= slideDownSpeed, AddSlideSpeed(velocity.z, hitNormal.z));
            } 
        }

        #region Life Cycle

        private void Update()
        {
            HandleGravity();
            SlopeDown();
            controller.Move(velocity * Time.deltaTime);
        }

        private void OnControllerColliderHit(ControllerColliderHit hit) => hitNormal = hit.normal;

        private void Awake() => controller = GetComponent<CharacterController>();

        #endregion
    }

}
