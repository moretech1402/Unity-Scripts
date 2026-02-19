using UnityEngine;

namespace MC.Core.Unity
{

    public abstract class ServiceInitializer : MonoBehaviour
    {
        public abstract void RegisterServices();
    }

    public abstract class ServiceInstaller : ServiceInitializer
    {
        public sealed override void RegisterServices()
        {
            Install();
        }

        protected abstract void Install();

        protected void Register<TService>(TService instance)
            where TService : class
        {
#pragma warning disable IDE0001
            // Explicit generic type is required to register the service by its contract (interface), not by implementation.
            ServiceLocator.Register<TService>(instance);
#pragma warning restore IDE0001
        }
    }

    [DefaultExecutionOrder(-1000)]
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

