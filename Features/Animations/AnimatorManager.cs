using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Animations
{
    public enum AnimatorParameters
    {
        DirectionX, DirectionY, Speed
    }

    public class AnimatorManager : Singleton<AnimatorManager>
    {

        [Header("Parameters")]
        [SerializeField] string directionX = "DirectionX";
        [SerializeField] string directionY = "DirectionY";
        [SerializeField] string speed = "Speed";

        Dictionary<AnimatorParameters, string> dict;

        public static int GetHash(AnimatorParameters parameter) => Animator.StringToHash(Instance.dict[parameter]);

        private void OnEnable()
        {
            dict = new Dictionary<AnimatorParameters, string>(){
            { AnimatorParameters.DirectionX, directionX }, { AnimatorParameters.DirectionY, directionY },
            { AnimatorParameters.Speed, speed }
        };
        }
    }

}
