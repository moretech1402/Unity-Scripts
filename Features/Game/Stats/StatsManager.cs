using System;
using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Stats
{
    public enum StatKeys
    {
        Level, XP,
        HP, MaxHP, MP, MaxMP, Stamina, MaxStamina,
        Strength, Dexterity, Mind, Agility,
        Attack, Defense, MagicAttack, MagicDefense,
        Precision, Evasion, MagicEvasion,
        MovementSpeed
    }

    public class StatsManager : Singleton<StatsManager>
    {
        [Header("Definition")]
        [SerializeField] StatDefinition level = new("lv", "Nivel");
        [SerializeField] StatDefinition 
        experiencePoints = new("xp", "Exp"),
        hitPoints = new("hp", "HP"), maxHitPoints = new("mhp", "Max HP"),
        manaPoints = new("mp", "MP"), maxManaPoints = new("mmp", "Max MP"),
        stamina = new("sta", "Energía"), maxStamina = new("msta", "Energía Máxima"),
        attack = new("atk", "Ataque"), magicAttack = new("mat", "Magic Attack"),
        defense = new("def", "Defensa"), magicDefense = new("mdf", "Defensa Mágica"),
        movementSpeed = new("mov", "Velocidad de Movimiento");

        Dictionary<StatKeys, StatDefinition> dict = new();

        public static Dictionary<StatKeys, StatDefinition> Dict => Instance.dict;

        private new void Awake()
        {
            dict = new()
            {
                { StatKeys.Level, level }, { StatKeys.XP, experiencePoints },
                { StatKeys.HP, hitPoints }, { StatKeys.MaxHP, maxHitPoints }, { StatKeys.MP, manaPoints }, { StatKeys.MaxMP, maxManaPoints }, { StatKeys.Stamina, stamina }, { StatKeys.MaxStamina, maxStamina },
                { StatKeys.Attack, attack }, { StatKeys.MagicAttack, magicAttack }, { StatKeys.Defense, defense }, { StatKeys.MagicDefense, magicDefense },
                { StatKeys.MovementSpeed, movementSpeed }
            };
        }

        public StatDefinition GetStatDefinition(StatKeys key)
        {
            if (dict.TryGetValue(key, out StatDefinition definition))
                return definition;

            Debug.LogWarning($"The key {key} does not have an associated definition.");
            return default;
        }

        public void PrintStats()
        {
            foreach (var entry in dict)
                Debug.Log($"{entry.Key}: {entry.Value.Name} ({entry.Value.Shortening})");
        }
    }

    [Serializable]
    public struct StatDefinition
    {
        public string Shortening, Name;

        public StatDefinition(string shortening, string name)
        {
            Shortening = shortening;
            Name = name;
        }
    }

    public struct StatInfo
    {
        public StatKeys key;
        public StatDefinition definition;

        public StatInfo(StatKeys key, StatDefinition definition)
        {
            this.key = key;
            this.definition = definition;
        }
    }
}
