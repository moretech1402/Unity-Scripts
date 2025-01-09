using UnityEngine;

namespace Animation
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class PartAnimator2D : PartAnimator
    {
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
            InitializeAnimator();

            // Hash parameter names
            directionXHash = AnimatorManager.GetHash(AnimatorParameters2D.DirectionX);
            directionYHash = AnimatorManager.GetHash(AnimatorParameters2D.DirectionY);
            speedHash = AnimatorManager.GetHash(AnimatorParameters2D.Speed);
        }
    }

}
