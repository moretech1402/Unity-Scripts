using UnityEngine;

namespace Animations
{
    [RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
    public class PartAnimator : MonoBehaviour
    {
        Animator animator;

        int directionXHash, directionYHash, speedHash;

        public void GOMoved(Vector2 move, float speed)
        {
            animator.SetFloat(directionXHash, move.x);
            animator.SetFloat(directionYHash, move.y);
            animator.SetFloat(speedHash, speed);
        }

        internal void GOStopped() => animator.SetFloat(speedHash, 0);

        private void OnEnable()
        {
            // Get animator
            if (animator == null) animator = GetComponent<Animator>();

            // Hash parameter names
            directionXHash = AnimatorManager.GetHash(AnimatorParameters.DirectionX);
            directionYHash = AnimatorManager.GetHash(AnimatorParameters.DirectionY);
            speedHash = AnimatorManager.GetHash(AnimatorParameters.Speed);
        }
    }

}
