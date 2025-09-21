using System.Collections.Generic;
using UnityEngine;

namespace MC.Core.Unity.Targeting
{
    public interface ITarget
    {
        Vector3 GetPosition();
        bool IsAlive { get; }
    }

    public interface ITargetRegistryService
    {
        void Register(ITarget target);
        void Unregister(ITarget target);
        ITarget GetClosest(Vector3 fromPosition);
    }

    /// <summary> Service that keeps track of all targets in the scene. </summary>
    public class TargetRegistryService : ITargetRegistryService
    {
        private readonly List<ITarget> targets = new();

        public void Register(ITarget target) => targets.Add(target);
        public void Unregister(ITarget target) => targets.Remove(target);

        public ITarget GetClosest(Vector3 fromPosition)
        {
            ITarget closest = null;
            float minDist = float.MaxValue;

            foreach (var t in targets)
            {
                if (!t.IsAlive) continue;

                float dist = Vector3.Distance(fromPosition, t.GetPosition());
                if (dist < minDist)
                {
                    minDist = dist;
                    closest = t;
                }
            }

            return closest;
        }
    }
}
