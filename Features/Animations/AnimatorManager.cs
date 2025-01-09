using System.Collections.Generic;
using UnityEngine;
using Core;
using System;

namespace Animation
{
    public abstract class AnimatorManager : Singleton<AnimatorManager>
    {
        protected Dictionary<Enum, string> dict;

        public static int GetHash(Enum parameter) => Animator.StringToHash(Instance.dict[parameter]);

        protected abstract void InitializeParameters();

        private void OnEnable() => InitializeParameters();
    }

}