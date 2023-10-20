using BlueGravityTest.ScriptableObjects.Items;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BlueGravityTest.Scripts.UI
{
    public class InventorySlot : MonoBehaviour, IPointerDownHandler,IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler, I_Slot
    {
        [SerializeField] private Image icon;

        private ItemData _itemData;
        private PlayerInventoryUi _playerInventoryUi;
        private IPointerEnterHandler _pointerEnterHandlerImplementation;
        private IPointerExitHandler _pointerExitHandlerImplementation;

        public void Initialize(ItemData data, PlayerInventoryUi playerInventoryUi)
        {
            _itemData = data;
            icon.sprite = data.itemIcon;
            _playerInventoryUi = playerInventoryUi;
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
            if ((ClothesData)_itemData == null)
                return;
            
            _itemData = data;
            icon.sprite = _itemData.itemIcon;
        }

        public ClothesData GetSlotClotheData()
        {
            if ((ClothesData)_itemData == null)
                return default;
            return (ClothesData)_itemData;
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

