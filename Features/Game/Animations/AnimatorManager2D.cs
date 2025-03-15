using System;
using System.Collections.Generic;
using Animation;
using UnityEngine;

namespace Animation
{
    public enum AnimatorParameters2D
    {
        DirectionX, DirectionY, Speed
    }

    public class AnimatorManager2D : AnimatorManager
    {
        [Header("Parameters")]
        [SerializeField] string directionX = "DirectionX";
        [SerializeField] string directionY = "DirectionY";
        [SerializeField] string speed = "Speed";

        protected override void InitializeParameters()
        {
            dict = new Dictionary<Enum, string>(){
            { AnimatorParameters2D.DirectionX, directionX }, { AnimatorParameters2D.DirectionY, directionY },
            { AnimatorParameters2D.Speed, speed }
        };

        }
    }

}
