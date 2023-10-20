using System;
using System.Collections;
using System.Collections.Generic;
using BlueGravityTest.ScriptableObjects.Items;
using BlueGravityTest.Scripts.MVC;
using BlueGravityTest.Scripts.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace BlueGravityTest.Scripts.UI
{
   public class PlayerInventoryUi : MonoBehaviour
   {
      [SerializeField] private GameObject panel;
      [SerializeField] private Transform content;
      [SerializeField] private List<ClotheEquippedSlot> clotheEquippedSlot;
      [SerializeField] private InventorySlot inventorySlotPrefab;
      [SerializeField] private Image slotDrawReference;
      
      private PlayerModel _player;
      private Dictionary<int, Action<PlayerModel>> _uiEventDeployer;
      private List<InventorySlot> _inventorySlotsCreated = new();
      private bool _onDraw;
      private I_Slot _slotPointerDown;
      private I_Slot _slotPointerEnter;
      private void Awake()
      {
         _uiEventDeployer = new Dictionary<int, Action<PlayerModel>>()
         {
            { (int)UICommands.OPEN_INVENTORY, OpenInventory },
         };
      }
   
      private void OpenInventory(PlayerModel player)
      {
         panel.SetActive(!panel.activeSelf);
         Cursor.visible = panel.activeSelf;
   
         if (!panel.activeSelf)
         {
            CloseInventory();
            return;
         }
         
         _player = player;
         var inventory =  _player.GetPlayerInventory();
         foreach (var clothesData in inventory.GetEquippedClothes())
         {
            foreach (var slot in clotheEquippedSlot)
               slot.SetSlotData(clothesData, this, _player);
         }
   
         foreach (var item in inventory.GetItems())
         {
            var newItemSlot = Instantiate(inventorySlotPrefab, content);
            newItemSlot.Initialize(item,this);
            _inventorySlotsCreated.Add(newItemSlot);
         }
      }
   
      private void CloseInventory()
      {
         foreach (var item in _inventorySlotsCreated)
            Destroy(item.gameObject);
         _inventorySlotsCreated.Clear();
      }

      public void SetSlotPointerDown(I_Slot slot)
      {
         if(slot.GetSlotClotheData() == default)
            return;
         _slotPointerDown = slot;
         StartCoroutine(SlotOnDraw(_slotPointerDown.GetSlotClotheData().itemIcon));
      }

      public void SetSlotPointerEnter(I_Slot slot)
      {
         if(slot.GetSlotClotheData() == default)
            return;
         _slotPointerEnter = slot;
      }

      public void RemoveSlotPointerEnter()
      {
         _slotPointerEnter = null;
      }
      
      public void ReplaceSlots()
      {
         if (_slotPointerDown == null || _slotPointerEnter == null)
         {
            _slotPointerDown = null;
            _slotPointerEnter = null;
            _onDraw = false;
            return;
         }
         var slotPointerDownData = _slotPointerDown.GetSlotClotheData();
         _slotPointerDown.ReplaceSlotData(_slotPointerEnter.GetSlotClotheData());
         _slotPointerEnter.ReplaceSlotData(slotPointerDownData);
         _slotPointerDown = null;
         _slotPointerEnter = null;
         _onDraw = false;
      }

      private IEnumerator SlotOnDraw(Sprite clotheSprite)
      {
         slotDrawReference.sprite = clotheSprite;
         slotDrawReference.gameObject.SetActive(true);
         _onDraw = true;
         while (_onDraw)
         {
            slotDrawReference.transform.position = Input.mousePosition;
            yield return null;
         }
         slotDrawReference.gameObject.SetActive(false);
      }
      
      public void UIEventHandler(UIParams parameters)
      {
         if (_uiEventDeployer.TryGetValue((int)parameters.Command, out var actionToPerform))
         {
            actionToPerform.Invoke(parameters.Player);
         }
      }
   }
}

