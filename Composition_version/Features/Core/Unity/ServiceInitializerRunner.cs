using UnityEngine;

namespace MC.Core.Unity
{

    public abstract class ServiceInitializer : MonoBehaviour
    {
        public abstract void RegisterServices();
    }

    public class ServiceInitializerRunner : MonoBehaviour
    {
        [Tooltip("List of service initializers to run on Awake. Order matters.")]
        [SerializeField] private ServiceInitializer[] _serviceInitializers;

        void Awake()
        {

            foreach (var initializer in _serviceInitializers)
            {
                initializer.RegisterServices();
            }
        }
    }
}

