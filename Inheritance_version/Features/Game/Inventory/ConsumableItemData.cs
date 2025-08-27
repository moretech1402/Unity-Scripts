using Skills;
using Stats;
using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(menuName = "Inventory/Consumable")]
    public class ConsumableItemData : ItemData
    {
        [SerializeField] ActiveSkillData effect;
        [SerializeField] int consumableAmount = 1;
        [SerializeField] bool masterSkill = false;

        public void Use(StatsHandlerBase caster, StatsHandlerBase target){
            effect.Execute(caster, target);
        }
    }

}
