using System.Collections;
using System.Collections.Generic;
using BlueGravityTest.ScriptableObjects.Items;
using BlueGravityTest.Scripts.MVC;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BlueGravityTest.Scripts.UI
{
    public class ShopSellButton : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text priceText;
        [SerializeField] private Button button;
        
        private ItemData _itemData;
        private PlayerModel _player;
        private ShopUiController _shopUiController;
        
        public void Initialize(ItemData data, PlayerModel player, ShopUiController shopUiController)
        {
            _itemData = data;
            _player = player;
            _shopUiController = shopUiController;
            icon.sprite = _itemData.itemIcon;
            priceText.text = _itemData.itemSellPrice.ToString();
            button.onClick.AddListener(SellItem);
        }

        private void SellItem()
        {
            _player.GetPlayerWallet().Sell(_itemData.itemSellPrice);
            _shopUiController.SetCurrentPlayerGoldText();
            _player.GetPlayerInventory().RemoveItem(_itemData);
            _shopUiController.AddNewPurchaseButton(_itemData);
            Destroy(gameObject);
        }
    }
}

