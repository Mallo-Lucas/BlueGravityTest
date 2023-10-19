using UnityEngine;

namespace BlueGravityTest.ScriptableObjects.Player
{
    [CreateAssetMenu(menuName = "ScriptableObjects/PlayerData", fileName = "PlayerData", order = 0)]
    public class PlayerData : ScriptableObject
    {
        public float playerSpeed;
    }
}

