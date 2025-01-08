using System.Collections;
using System.Collections.Generic;
using Core.Events;
using UnityEngine;

namespace Animation
{
    [RequireComponent(typeof(Animator))]
    public class PartAnimator3D : MonoBehaviour
    {
        Animator animator;
        void Animate(Vector2 vector)
        {
            animator.SetFloat("Speed", vector.magnitude);
        }

        private void Start()
        {
            animator = GetComponent<Animator>();
            EventManager.OnInputMove += Animate;
        }

        private void OnDestroy()
        {
            EventManager.OnInputMove -= Animate;
        }
    }

}
