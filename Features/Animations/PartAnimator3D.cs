using System;
using Core;
using Core.Events;
using UnityEngine;

namespace Animation
{
    public class PartAnimator3D : PartAnimator
    {
        int speedHash, isRunningHash, isGroundedHash;

        void AnimateMove(int goID, bool isRunning)
        {
            if (goID == gameObject.GetInstanceID())
            {
                animator.SetFloat(speedHash, 1);
                animator.SetBool(isRunningHash, isRunning);
            }
        }

        void AnimateStop(int goID)
        {
            if (goID == gameObject.GetInstanceID())
                animator.SetFloat(speedHash, 0);
        }

        private void IsGrounded(int goID, bool isGrounded)
        {
            if(goID == gameObject.GetInstanceID())
                animator.SetBool(isGroundedHash, isGrounded);
        }

        private void Start()
        {
            InitializeAnimator();

            speedHash = AnimatorManager.GetHash(AnimatorParameters3D.Speed);
            isRunningHash = AnimatorManager.GetHash(AnimatorParameters3D.IsRunning);
            isGroundedHash = AnimatorManager.GetHash(AnimatorParameters3D.IsGrounded);

            EventManager.OnMove += AnimateMove;
            EventManager.OnStopGO += AnimateStop;
            EventManager.OnIsGrounded += IsGrounded;
        }

        private void OnDestroy()
        {
            EventManager.OnMove -= AnimateMove;
            EventManager.OnStopGO -= AnimateStop;
            EventManager.OnIsGrounded -= IsGrounded;
        }
    }

}
