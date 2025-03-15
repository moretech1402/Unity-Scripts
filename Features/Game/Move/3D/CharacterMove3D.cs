using System;
using System.Collections.Generic;
using Core.Contracts;
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
        #region Variables

        [SerializeField] float defaultSpeed = 5, defaultRunMult = 2, defaultJumpForce = 5f, slideSpeed = 7, slideDownSpeed = 10;
        [SerializeField] StatsProvider statsProvider;

        Dictionary<MoveStatKeys, float> stats = new();

        float speed;
        bool isRunning, oldIsGrounded = true;
        Vector3 velocity, hitNormal;

        const float GRAVITY_VALUE = -9.81f;

        CharacterController controller;

        bool IsGrounded => controller.isGrounded;

        #endregion

        #region Methods

        public void Jump()
        {
            if (controller.isGrounded)
                velocity.y = stats[MoveStatKeys.JumpForce];
        }

        public void Run(bool running) => isRunning = running;

        public void Move(Vector3 movement)
        {
            if(!IsGrounded) return;
            // Calculate Speed
            speed = movement == Vector3.zero ? 0 : stats[MoveStatKeys.MovementSpeed];
            var runMult = isRunning ? defaultRunMult : 1;

            // To Avoid speed increase when diagonal move
            var normalizedMovement = Vector3.ClampMagnitude(movement, 1);

            // Rotation to target
            transform.LookAt(transform.position + normalizedMovement);

            var finalMove = speed * runMult * normalizedMovement;
            velocity = new(finalMove.x, velocity.y, finalMove.z);

            // Notify movement
            MovementState state;
            if (finalMove.magnitude <= 0) state = MovementState.Stopped;
            else state = isRunning ? MovementState.Running : MovementState.Walking;
            MoveEventManager.ComplexMove(gameObject.GetInstanceID(), state);
        }

        private void HandleGravity()
        {
            var yAceleration = GRAVITY_VALUE * Time.deltaTime;
            velocity.y += yAceleration;
            
            if (IsGrounded && velocity.y < 0) velocity.y = -1;

            if (oldIsGrounded != IsGrounded)
            {
                oldIsGrounded = IsGrounded;
                MoveEventManager.IsGrounded(gameObject.GetInstanceID(), IsGrounded);
            }
        }

        void SlopeDown()
        {
            var isOnSlope = Vector3.Angle(Vector3.up, hitNormal) >= controller.slopeLimit;
            if (isOnSlope)
            {
                float AddSlideSpeed(float current, float axis) => current + (1f - hitNormal.y) * axis * slideSpeed;
                velocity = new(AddSlideSpeed(velocity.x, hitNormal.x), velocity.y -= slideDownSpeed, AddSlideSpeed(velocity.z, hitNormal.z));
            }
        }

        void UpdateStats(){
            bool hasProvider = statsProvider != null;
            stats = new(){
                { MoveStatKeys.MovementSpeed, hasProvider ? statsProvider.GetMovementSpeed() : defaultSpeed },
                { MoveStatKeys.JumpForce, hasProvider ? statsProvider.GetJumpForce() : defaultJumpForce }
            };
        }

        #endregion

        #region Life Cycle

        private void Update()
        {
            HandleGravity();
            SlopeDown();
            controller.Move(velocity * Time.deltaTime);
        }

        private void OnControllerColliderHit(ControllerColliderHit hit) => hitNormal = hit.normal;

        private void Awake() {
            controller = GetComponent<CharacterController>();
            UpdateStats();
        }

        #endregion
    }

}
