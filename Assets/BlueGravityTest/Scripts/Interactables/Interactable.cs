using BlueGravityTest.Scripts.MVC;
using UnityEngine;

namespace BlueGravityTest.Scripts.Interactables
{
    public class Interactable : MonoBehaviour
    {
        [SerializeField] private GameObject interactFeedback;
    
        public void ShowInteractFeedback(bool state)
        {
            interactFeedback.SetActive(state);
        }

        public virtual void Interact(PlayerModel model)
        {
            
        }
    }
}

