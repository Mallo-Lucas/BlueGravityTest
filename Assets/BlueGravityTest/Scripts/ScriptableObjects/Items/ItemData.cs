using UnityEngine;

namespace BlueGravityTest.ScriptableObjects.Items
{
    [CreateAssetMenu(menuName = "ScriptableObjects/ItemData", fileName = "ItemData", order = 1)]
    public class ItemData : ScriptableObject
    {
        public Sprite itemIcon;
        public int itemSellPrice;
        public int itemPurchasePrice;
    }
}

