using UnityEngine;

namespace InventorySystem
{
    public class ItemData : ScriptableObject
    {
        [SerializeField] protected string _name = "Poción";
        [SerializeField] protected Sprite icon;
    }
}
