using System;
using System.Collections.Generic;
using Core.Chemistry;
using MC.Core.Unity.UI;
using MC.Game.Inventory;
using UnityEngine;
using UnityEngine.Pool;

namespace MC.Game.Alchemy
{
    public class AlchemyMixSpeciesUI : MonoBehaviour
    {
        [SerializeField] Transform _parentTransform;
        [SerializeField] MixPanelInventoryItemUI _inventoryItemPrefab;

        [SerializeField] GameObject _plusSignPrefab;

        ObjectPool<MixPanelInventoryItemUI> _pool;

        private ObjectPool<GameObject> _plusSignPool;

        private readonly Dictionary<ChemicalSpeciesSO, MixPanelInventoryItemUI> _activeItems = new();
        private readonly List<GameObject> _plusSigns = new();

        public ChemicalSpeciesSO ResultProduct { get; private set; }

        void Awake()
        {
            UIUtils.Fresh(_parentTransform);

            _pool = new ObjectPool<MixPanelInventoryItemUI>(
                () => Instantiate(_inventoryItemPrefab, _parentTransform),
                null,
                item => item.gameObject.SetActive(false)
            );

            _plusSignPool = new ObjectPool<GameObject>(
                () => Instantiate(_plusSignPrefab, _parentTransform),
                null,
                plusSign => plusSign.SetActive(false)
            );
        }

        internal void AddMonomer(InventoryItem inventoryProduct)
        {
            if (_activeItems.TryGetValue(inventoryProduct.Item, out var existingItem))
            {
                existingItem.AddAmount(inventoryProduct.Amount);
                return;
            }

            if (_activeItems.Count > 0)
            {
                var plusSign = _plusSignPool.Get();
                plusSign.SetActive(true);
                _plusSigns.Add(plusSign);
            }

            var newItemUI = _pool.Get();
            newItemUI.Setup(inventoryProduct);
            newItemUI.gameObject.SetActive(true);
            _activeItems.Add(inventoryProduct.Item, newItemUI);
        }

        public void ClearPanel()
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

        internal MoleculesAmountPair[] GetProductPairs()
        {
            List<MoleculesAmountPair> productPairs = new();
            foreach (var inventoryItem in _activeItems.Values)
            {
                productPairs.Add(new MoleculesAmountPair()
                {
                    Molecule = new(inventoryItem.Item.Atoms),
                    Amount = inventoryItem.Amount
                });
            }
            return productPairs.ToArray();
        }

        internal void SetResultProduct(ChemicalSpeciesSO product)
        {
            ResultProduct = product;
            ClearPanel();
        }
    }
}
