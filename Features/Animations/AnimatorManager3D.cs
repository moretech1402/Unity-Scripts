using System.Collections.Generic;
using UnityEngine;
using System;

namespace Animation
{
    public enum AnimatorParameters3D
    {
        Speed, IsRunning, IsGrounded
    }

    public class AnimatorManager3D : AnimatorManager
    {

        [Header("Parameters")]
        [SerializeField] string speed = "Speed";
        [SerializeField] string isRunning = "IsRunning";
        [SerializeField] string isGrounded = "IsGrounded";

        protected override void InitializeParameters()
        {
            dict = new Dictionary<Enum, string>
            {
                { AnimatorParameters3D.Speed, speed },
                { AnimatorParameters3D.IsRunning, isRunning },
                { AnimatorParameters3D.IsGrounded, isGrounded }
            };
        }
    }

}
