using MC.Game.Inventory;
using TMPro;
using UnityEngine;

namespace MC.Game.Alchemy
{
    public class MixPanelInventoryItemUI : MonoBehaviour
    {
        [SerializeField] InventoryItemUI _inventoryItemUI;
        [SerializeField] TextMeshProUGUI _formulaDisplay;

        public ChemicalSpeciesSO Item => _inventoryItemUI.Item;
        public int Amount => _inventoryItemUI.Amount;

        internal void AddAmount(int amount)
        {
            _inventoryItemUI.Add(amount);
        }

        internal void Setup(InventoryItem product)
        {
            _inventoryItemUI.SetInventoryItem(product);
            _formulaDisplay.SetText(product.Item.Formula);
        }
    }
}
