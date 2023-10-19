using System;
using System.Collections.Generic;
using UnityEngine;

namespace BlueGravityTest.ScriptableObjects.Items
{
    [CreateAssetMenu(menuName = "ScriptableObjects/ShopData", fileName = "ShopData", order = 3)]
    public class ShopData : ScriptableObject
    {
        public List<ItemToSellData> ItemsToSell;
    }

    [Serializable]
    public struct ItemToSellData
    {
        public ItemData ItemToSell;
        public int AmountOfTheItem;
    }
}

