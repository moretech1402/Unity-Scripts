using System;
using System.Collections.Generic;
using System.Linq;
using Core.Chemistry;
using MC.Core.Unity.UI;
using MC.Game.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;

namespace MC.Game.Alchemy
{
    public class AlchemyReactantsPanelUI : MonoBehaviour
    {
        [SerializeField] private Transform _parentTransform;
        [SerializeField] private InteractableInventoryItemUI _inventoryItemPrefab;
        [SerializeField] private TextMeshProUGUI _plusSignPrefab;

        public Action<InventoryItem> OnRemoveItem;

        private ObjectPool<InteractableInventoryItemUI> _pool;
        private ObjectPool<TextMeshProUGUI> _plusSignPool;
        private readonly Dictionary<ChemicalSpeciesSO, InteractableInventoryItemUI> _activeItems = new();
        private readonly List<TextMeshProUGUI> _plusSigns = new();

        void Awake()
        {
            UIUtils.Fresh(_parentTransform);
            _pool = CreatePool(_inventoryItemPrefab, _parentTransform);
            _plusSignPool = CreatePool(_plusSignPrefab, _parentTransform);
        }

        internal void AddMonomer(InventoryItem inventoryItem)
        {
            if (_activeItems.TryGetValue(inventoryItem.Item, out var existingItem))
            {
                existingItem.Add(inventoryItem.Amount);
                return;
            }

            if (_activeItems.Count > 0)
            {
                var plusSign = _plusSignPool.Get();
                plusSign.gameObject.SetActive(true);
                _plusSigns.Add(plusSign);
            }

            var newItemUI = _pool.Get();
            newItemUI.Setup(inventoryItem);

            newItemUI.SetOnClickAction(() => Return(newItemUI));

            newItemUI.gameObject.SetActive(true);
            _activeItems.Add(inventoryItem.Item, newItemUI);
        }

        internal void ClearPanel()
        {
            foreach (var item in _activeItems.Values)
            {
                _pool.Release(item);
            }
            _activeItems.Clear();

            foreach (var plusSign in _plusSigns)
            {
                _plusSignPool.Release(plusSign);
            }
            _plusSigns.Clear();
        }

        internal MoleculesAmountPair[] GetReactantPairs()
        {
            return _activeItems.Values
                .Select(interactableInventoryItem => new MoleculesAmountPair()
                {
                    Molecule = new(interactableInventoryItem.Item.Atoms),
                    Amount = interactableInventoryItem.Amount
                })
                .ToArray();
        }

        internal void ReturnAll()
        {
            var itemsToReturn = new List<InteractableInventoryItemUI>(_activeItems.Values);

            foreach (var itemUI in itemsToReturn)
            {
                Return(itemUI, itemUI.Amount);
            }
        }

        private ObjectPool<T> CreatePool<T>(T prefab, Transform parentTransform) where T : MonoBehaviour
        {
            return new ObjectPool<T>(
                () => Instantiate(prefab, parentTransform),
                null,
                item => item.gameObject.SetActive(false)
            );
        }

        private void Return(InteractableInventoryItemUI itemUI, int amount = 1)
        {
            itemUI.Add(-amount);

            OnRemoveItem(new InventoryItem(itemUI.Item, amount));

            if (itemUI.Amount <= 0)
            {
                RemoveItem(itemUI);
            }
        }

        private void RemoveItem(InteractableInventoryItemUI itemUI)
        {
            _pool.Release(itemUI);
            _activeItems.Remove(itemUI.Item);

            if (_activeItems.Count > 0)
            {
                var lastPlusSign = _plusSigns[^1];
                _plusSignPool.Release(lastPlusSign);
                _plusSigns.RemoveAt(_plusSigns.Count - 1);
            }
        }
    }
}