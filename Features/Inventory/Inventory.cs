using System;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    [Serializable]
    public class Inventory
    {
        [SerializeField] int maxStack = 99, spaces = 16;
        [SerializeField] List<InventoryItem> items;
    }

    [Serializable]
    public struct InventoryItem{
        [SerializeField] ItemData item;
        [SerializeField] float amount;
    }

}
