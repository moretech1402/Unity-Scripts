using UnityEngine;

namespace Animation
{
    [RequireComponent(typeof(Animator))]
    public class PartAnimator : MonoBehaviour
    {
        protected Animator animator;

        private void Awake() => animator = GetComponent<Animator>();
    }

}
