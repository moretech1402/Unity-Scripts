using System.Collections.Generic;
using UnityEngine;

namespace MC.Core.Unity.Cameras.Rig
{
    public static class CameraRigRegistry
    {
        static readonly Dictionary<CameraContext, CameraRig> _rigs = new();

        public static void Register(CameraContext context, CameraRig rig)
        {
            _rigs[context] = rig;
        }

        public static CameraRig Get(CameraContext context)
        {
            _rigs.TryGetValue(context, out var rig);
            return rig;
        }

        internal static void Unregister(CameraContext context)
        {
            if (!_rigs.Remove(context))
            {
                Debug.LogWarning($"CameraContext {context.Id} was not registered.");
            }

            _rigs.Remove(context);
        }
    }
}
