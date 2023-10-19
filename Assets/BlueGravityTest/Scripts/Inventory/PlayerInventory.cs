using System.Collections;
using System.Collections.Generic;
using BlueGravityTest.ScriptableObjects.Items;
using UnityEngine;

namespace BlueGravityTest.Scripts.Inventory
{
    public class PlayerInventory
    {
        private List<ClothesData> _equippedClothes = new();
        private List<ItemData> _items = new();

        public List<ItemData> GetItems() => _items;
        public List<ClothesData> GetEquippedClothes() => _equippedClothes;
        
        public PlayerInventory(List<ClothesData> equippedClothes)
        {
            _equippedClothes.AddRange(equippedClothes);
        }

        public void AddItem(ItemData item)
        {
            _items.Add(item);
        }

        public void RemoveItem(ItemData item)
        {
            _items.Remove(item);
        }
        
    }
}

