using System;
using UnityEngine;

namespace Ailments
{
    [Serializable]
    public abstract class Ailment : ScriptableObject
    {
        protected Interval duration, applyInterval;
        protected Ailment[] secondaryAilments;

        public abstract void Apply();
    }

    public class Interval{
        public enum StackAcumulation{
            OnAdded, OnTick, OnHit, OnBeHitted
        }

        [SerializeField] StackAcumulation type = StackAcumulation.OnTick;
        [SerializeField] int stacksGoal = 5;
    }
}
