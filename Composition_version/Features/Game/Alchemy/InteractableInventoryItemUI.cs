using System;
using MC.Game.Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace MC.Game.Alchemy
{
    public class InteractableInventoryItemUI : MonoBehaviour
    {
        [SerializeField] InventoryItemUI _inventoryItemUI;
        [SerializeField] Button _button;

        Action _onClickAction;

        public ChemicalSpeciesSO Item => _inventoryItemUI.Item;
        public int Amount => _inventoryItemUI.Amount;

        public InventoryItem InventoryItem => _inventoryItemUI.InventoryItem;

        void Awake()
        {
            if (_button != null)
                _button.onClick.AddListener(() =>
                {
                    _onClickAction?.Invoke();
                });
        }

        internal void Add(int amount) => _inventoryItemUI.Add(amount, false);

        internal void Setup(InventoryItem inventoryItem) =>
            _inventoryItemUI.SetInventoryItem(inventoryItem);

        internal void SetOnClickAction(Action value) => _onClickAction = value;
    }
}
