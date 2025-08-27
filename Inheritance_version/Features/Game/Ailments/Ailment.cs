using System;
using Core.Contracts;
using UnityEngine;

namespace Ailments
{
    [Serializable]
    public abstract class Ailment : ScriptableObject
    {
        [SerializeField] protected Interval[] duration, applyInterval;
        [SerializeField] protected Ailment[] secondaryAilments;

        public abstract void Apply();
    }

    [Serializable]
    public class Interval{
        public enum StackAcumulation{
            OnAdded, OnTick, OnHit, OnBeHitted, OnRemoved
        }

        [SerializeField] StackAcumulation type = StackAcumulation.OnTick;
        [SerializeField] Vector2Int stacksGoal = new(2, 5);
    }

    [Serializable]
    public class AilmentMod : KeyMod<Ailment>
    {
        public AilmentMod(Ailment key, string formula = "0") : base(key, formula) { }

        public override float GetValue()
        {
            throw new NotImplementedException();
        }
    }
}
