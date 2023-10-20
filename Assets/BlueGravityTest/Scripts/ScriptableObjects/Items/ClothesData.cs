using BlueGravityTest.Scripts.MVC;
using UnityEditor.Animations;
using UnityEngine;

namespace BlueGravityTest.ScriptableObjects.Items
{
    [CreateAssetMenu(menuName = "ScriptableObjects/ClothesData", fileName = "ClothesData", order = 2)]
    public class ClothesData : ItemData
    {
        public PlayerView.BodyParts bodyParts;
        public RuntimeAnimatorController clothesAnimatorController;
    }
}
