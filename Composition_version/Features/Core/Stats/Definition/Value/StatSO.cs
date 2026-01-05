using System;
using UnityEngine;

namespace MC.Core.Stats.Definition
{
    public interface IStatDefinition
    {
        public Stat Stat { get; }
    }

    [Serializable]
    public struct StatData : IStatDefinition
    {
        public StatIdDefinitionSO Id;
        public float Value;

        Stat _cached;

        public Stat Stat
        {
            get
            {
                _cached ??= new(Id.StatId, new(Value));
                return _cached;
            }
        }
    }

    [CreateAssetMenu(menuName = "Core/Stats/Create new Stat with Value")]
    public class StatSO : ScriptableObject, IStatDefinition
    {
        public StatIdDefinitionSO Id;
        public float Value;

        Stat _cached;

        public Stat Stat
        {
            get
            {
                _cached ??= new(Id.StatId, new(Value));
                return _cached;
            }
        }
    }
}
