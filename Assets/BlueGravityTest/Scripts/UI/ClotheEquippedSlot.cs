using System.Collections;
using System.Collections.Generic;
using BlueGravityTest.ScriptableObjects.Items;
using BlueGravityTest.Scripts.MVC;
using BlueGravityTest.Scripts.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BlueGravityTest.Scripts.UI
{
    public class ClotheEquippedSlot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler,
        IPointerExitHandler, I_Slot
    {
        [SerializeField] private PlayerView.BodyParts bodyPart;
        [SerializeField] private Image clotheIcon;
        [SerializeField] private Image playerImageReference;
        private PlayerInventoryUi _playerInventoryUi;
        private ClothesData _clothesData;
        private PlayerModel _player;
        public void SetSlotData(ClothesData data, PlayerInventoryUi playerInventoryUi, PlayerModel player)
        {
            if (data.bodyParts != bodyPart)
                return;

            clotheIcon.sprite = data.itemIcon;
            playerImageReference.sprite = data.itemIcon;
            _playerInventoryUi = playerInventoryUi;
            _clothesData = data;
            _player = player;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _playerInventoryUi.SetSlotPointerDown(this);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _playerInventoryUi.ReplaceSlots();
        }

        public void ReplaceSlotData(ClothesData data)
        {
            _clothesData = data;
            clotheIcon.sprite = _clothesData.itemIcon;
            playerImageReference.sprite = _clothesData.itemIcon;
            _player.SetClotheHandler(_clothesData.bodyParts, _clothesData.clothesAnimatorController);
        }

        public ClothesData GetSlotClotheData()
        {
            return _clothesData;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _playerInventoryUi.SetSlotPointerEnter(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _playerInventoryUi.RemoveSlotPointerEnter();
        }
    }
}
