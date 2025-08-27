using Affinities;
using Ailments;
using Core.Utils;
using Skills.Effects;
using Stats;
using UnityEngine;

namespace InventorySystem
{
    public enum EquipPart{ Weapon, Ammo, Hand, Head, Chest, Legs, Shoes, Accesory }

    [CreateAssetMenu(menuName = "Inventory/Equipment/Generic")]
    public class EquipmentItemData : ItemData
    {
        [SerializeField] StatMod[] statsMod = new StatMod[]{ new (StatKeys.Attack, "10") };
        [SerializeField] SerializableDictionary<StatKeys, float> requirements;
        [SerializeField] AffinityMod[] affinityWeakMod;
        [SerializeField] AilmentMod[] ailmentWeakMod;
        [SerializeField] OnIntervalEffect[] extraEffects;
        [SerializeField] protected EquipPart equipPart = EquipPart.Chest;
//        [SerializeField] AspectData aspect;
    }

}
