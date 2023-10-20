using System.Collections.Generic;
using BlueGravityTest.ScriptableObjects.Items;
using UnityEngine;

namespace BlueGravityTest.ScriptableObjects.Player
{
    [CreateAssetMenu(menuName = "ScriptableObjects/PlayerData", fileName = "PlayerData", order = 0)]
    public class PlayerData : ScriptableObject
    {
        public float playerSpeed;
        public float  playerInteractRadius;
        public int  defaultGold;
        public List<ClothesData> defaultClothes;
    }
}

