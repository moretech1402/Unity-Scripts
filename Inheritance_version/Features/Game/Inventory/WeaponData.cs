using InventorySystem;
using Skills;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Equipment/Weapon")]
public class WeaponData : EquipmentItemData
{
    [SerializeField] AmmoTypes ammoDependency = AmmoTypes.None;
    [SerializeField] ActiveSkillData basicAttack;

    private void OnValidate() {
        equipPart = EquipPart.Weapon;
    }
}
