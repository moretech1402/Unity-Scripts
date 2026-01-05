using MC.Core;
using MC.Core.Patterns;
using MC.Core.Services;
using MC.Core.Unity;
using MC.Core.Unity.Services;

namespace MC.Game
{
    public class GameBootServicesInit : ServiceInitializer
    {
        public override void RegisterServices()
        {
            ServiceLocator.Register<IEventBus>(new EventBus());
            ServiceLocator.Register<IRandomService>(new UnityRandomService());
        }
    }

}
