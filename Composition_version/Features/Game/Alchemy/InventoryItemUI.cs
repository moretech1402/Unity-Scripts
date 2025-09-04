using MC.Game.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace MC.Game.Alchemy
{
    public class InventoryItemUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _amountText;
        [SerializeField] UnityEvent<Sprite> _onIconChanged;
        [SerializeField] UnityEvent<string> _onFormulaChanged;
        [SerializeField] UnityEvent<string> _onInfoRequested;

        public InventoryItem InventoryItem { get; private set; }

        public ChemicalSpeciesSO Item => InventoryItem?.Item;
        public int Amount => InventoryItem?.Amount ?? 0;

        public void Add(int amount, bool minValueRestriction = true)
        {
            if (InventoryItem == null) return;

            var minValue = 1;
            var maxValue = 99;
            var totalValue = InventoryItem.Amount + amount;
            if ((minValueRestriction && totalValue < minValue) || totalValue > maxValue) return;

            InventoryItem.Add(amount);
            UpdateAmountText();
        }

        public void RequestInfo()
        {
            if (InventoryItem == null)
            {
                _onInfoRequested?.Invoke(string.Empty);
                return;
            }

            var item = InventoryItem.Item;
            _onInfoRequested?.Invoke($"{item.Name}\n({item.Formula})");
        }

        public void UpdateIcon()
        {
            if (InventoryItem == null)
            {
                _onIconChanged?.Invoke(null);
                return;
            }

            _onIconChanged?.Invoke(InventoryItem.Item.Icon);
        }

        public void SetInventoryItem(InventoryItem inventoryItem)
        {
            if (inventoryItem == null)
            {
                _amountText.text = string.Empty;
                return;
            }

            InventoryItem = inventoryItem;
            UpdateIcon();
            UpdateAmountText();
            _onFormulaChanged?.Invoke(inventoryItem.Item.Formula);
        }

        private void UpdateAmountText()
        {
            _amountText.SetText(InventoryItem == null ?
                string.Empty : InventoryItem.Amount.ToString());
        }
    }
}
