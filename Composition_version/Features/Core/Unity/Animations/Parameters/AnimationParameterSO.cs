using UnityEngine;

namespace MC.Core.Unity.Animation.Parameters
{
    [CreateAssetMenu(menuName = "Core/Animation/Parameter")]
    public sealed class AnimationParameterSO : ScriptableObject
    {
        [SerializeField] private string _name;
        private int _hash;

        public int Hash
        {
            get
            {
                if (_hash == 0)
                    _hash = Animator.StringToHash(_name);
                return _hash;
            }
        }
    }
}
