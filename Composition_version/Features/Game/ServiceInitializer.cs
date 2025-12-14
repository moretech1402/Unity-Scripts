using MC.Core;
using MC.Core.Unity.Animations;
using MC.Core.Unity.Targeting;
using MC.Game.Characters;
using UnityEngine;

namespace MC.Game{

    [Obsolete("Use individual Initializers for each feature module")]
    public class ServiceInitializer : MonoBehaviour
    {
        [SerializeField] private ClassSO[] _initialClasses;

        private void Awake()
        {
            ServiceLocator.Register<ICharacterService>(new CharacterService(_initialClasses));
            ServiceLocator.Register<ITweenService>(new DOTweenService());
            ServiceLocator.Register<ITargetRegistryService>(new TargetRegistryService());
        }
    }
}

