using System.Collections;
using System.Collections.Generic;
using BlueGravityTest.ScriptableObjects.Items;
using BlueGravityTest.Scripts.Inventory;
using BlueGravityTest.Scripts.MVC;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopPurchaseButton : MonoBehaviour
{
   [SerializeField] private Image itemIcon;
   [SerializeField] private TMP_Text priceText;
   [SerializeField] private TMP_Text amountOfTheItemText;
   [SerializeField] private Button button;

   private int _price;
   private int _amountOfTheItem;
   private PlayerModel _player;
   private ShopUiController _shopUiController;
   public void Initialize(ItemData data, int amountOfTheItem, PlayerModel player, ShopUiController shopUiController)
   {
      itemIcon.sprite = data.itemIcon;
      _price = data.itemPurchasePrice;
      priceText.text = _price.ToString();
      _amountOfTheItem = amountOfTheItem;
      amountOfTheItemText.text = _amountOfTheItem.ToString();
      _player = player;
      _shopUiController = shopUiController;
      button.onClick.AddListener(PurchaseItem);
   }

   private void PurchaseItem()
   {
      if (!_player.GetPlayerWallet().Purchase(_price))
         return;
      _amountOfTheItem--;
      amountOfTheItemText.text = _amountOfTheItem.ToString();
      _shopUiController.SetCurrentPlayerGoldText();
      if (_amountOfTheItem <= 0)
         button.interactable = false;
   }
}
