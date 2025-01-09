using UnityEngine;

namespace Animation
{
    [RequireComponent(typeof(Animator))]
    public class PartAnimator : MonoBehaviour
    {
        protected Animator animator;

        protected virtual void InitializeAnimator()
        {
            animator = GetComponent<Animator>();
        }
    }

}
