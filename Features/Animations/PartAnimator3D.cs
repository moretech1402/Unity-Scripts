using System;
using Core.Events;

namespace Animation
{
    public class PartAnimator3D : PartAnimator
    {
        int speedHash, isGroundedHash;
        int isRunningHash; // deprecated


        void AnimateMove(int goID, MovementState movementState)
        {
            if (goID == gameObject.GetInstanceID())
                animator.SetFloat(speedHash, (float)movementState);
        } 

        private void IsGrounded(int goID, bool isGrounded)
        {
            if(goID == gameObject.GetInstanceID())
                animator.SetBool(isGroundedHash, isGrounded);
        }

        private void Start()
        {
            speedHash = AnimatorManager.GetHash(AnimatorParameters3D.Speed);
            isRunningHash = AnimatorManager.GetHash(AnimatorParameters3D.IsRunning); // deprecated
            isGroundedHash = AnimatorManager.GetHash(AnimatorParameters3D.IsGrounded);

            MoveEventManager.OnComplexMove += AnimateMove;
            MoveEventManager.OnIsGrounded += IsGrounded;
        }

        private void OnDestroy()
        {
            MoveEventManager.OnComplexMove -= AnimateMove;
            MoveEventManager.OnIsGrounded -= IsGrounded;
        }

        #region Legacy

        [Obsolete ("Use Movement State")]
        void AnimateMove(int goID, bool isRunning)
        {
            if (goID == gameObject.GetInstanceID())
            {
                animator.SetFloat(speedHash, 1);
                animator.SetBool(isRunningHash, isRunning);
            }
        }

        [Obsolete ("Use Animate Move with Movement State insteed")]
        void AnimateStop(int goID)
        {
            if (goID == gameObject.GetInstanceID())
                animator.SetFloat(speedHash, 0);
        }

        #endregion
    }

}
