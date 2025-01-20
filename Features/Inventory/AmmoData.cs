using UnityEngine;

namespace InventorySystem
{
    public enum AmmoTypes { None, Arrow }

    [CreateAssetMenu(menuName = "Inventory/Equipment/Ammo")]
    public class AmmoData : EquipmentItemData
    {
        [SerializeField] AmmoTypes type = AmmoTypes.Arrow;

        private void OnValidate() {
        equipPart = EquipPart.Ammo;
    }
    }

}
