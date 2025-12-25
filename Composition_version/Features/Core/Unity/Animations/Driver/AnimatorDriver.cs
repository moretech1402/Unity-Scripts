using UnityEngine;

namespace MC.Core.Unity.Animation.Driver
{
    public interface IAnimationDriver
    {
        void SetFloat(int parameter, float value);
        void SetBool(int parameter, bool value);
        void SetTrigger(int parameter);
        void SetInt(int parameter, int value);
    }

    public sealed class AnimatorDriver : IAnimationDriver
    {
        private readonly Animator _animator;

        public AnimatorDriver(Animator animator)
        {
            _animator = animator;
        }

        public void SetFloat(int parameter, float value) =>
            _animator.SetFloat(parameter, value);

        public void SetBool(int parameter, bool value) =>
            _animator.SetBool(parameter, value);

        public void SetTrigger(int parameter) =>
            _animator.SetTrigger(parameter);

        public void SetInt(int parameter, int value) =>
            _animator.SetInteger(parameter, value);
    }
}
