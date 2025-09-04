using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Core.Extensions;
using MC.Game.Alchemy;

namespace MC.Game.Inventory
{
    public interface IInventoryService
    {
        IEnumerable<InventoryItem> Inventory { get; }

        void Add(ChemicalSpeciesSO item, int amount = 1);

        void Add(InventoryItem inventoryItem) => Add(inventoryItem.Item, inventoryItem.Amount);

        void Remove(ChemicalSpeciesSO item, int amount = 1);
    }

    public class InventoryService : IInventoryService
    {
        const int _initialInventorySize = 20;
        readonly Vector2 _amountRange = new(10, 20);

        readonly Dictionary<ChemicalSpeciesSO, int> _inventory = new();

        public IEnumerable<InventoryItem> Inventory =>
            _inventory.Select(kvp => new InventoryItem(kvp.Key, kvp.Value));

        public InventoryService(ChemicalSpeciesSO[] possibleChemicals)
        {
            ChemicalSpeciesSO[] uniqueChemicals = (ChemicalSpeciesSO[])possibleChemicals
                .Distinct().ToArray().Shuffle();
            Queue<ChemicalSpeciesSO> uniqueChemicalsQueue = new(uniqueChemicals);

            int chemicalsCount = uniqueChemicalsQueue.Count;
            var iterations = Math.Min(_initialInventorySize, chemicalsCount);
            var random = new Random();

            _inventory.Clear();
            for (int i = 0; i < iterations; i++)
            {
                var chemical = uniqueChemicalsQueue.Dequeue();

                var amount = random.Next((int)_amountRange.X, (int)_amountRange.Y + 1);
                _inventory[chemical] = amount;
            }
        }

        public void Add(ChemicalSpeciesSO item, int amount = 1) => SetAmount(item, amount);

        public void Remove(ChemicalSpeciesSO item, int amount = 1) => SetAmount(item, -amount);

        private void SetAmount(ChemicalSpeciesSO item, int amount)
        {
            _inventory[item] = Math.Max(0, amount);

            if (_inventory[item] <= 0)
                _inventory.Remove(item);
        }
    }

    public class InventoryItem
    {
        public ChemicalSpeciesSO Item { get; set; }
        public int Amount { get; private set; }

        public int Value => Item.Value * Amount;

        public InventoryItem(ChemicalSpeciesSO item, int amount)
        {
            Item = item;
            SetAmount(amount);
        }

        private void SetAmount(int amount) => Amount = Math.Max(0, amount);

        internal void Add(int amount) => SetAmount(Amount + amount);

        public override string ToString() => $"{Item.name} x{Amount}";
    }

}
