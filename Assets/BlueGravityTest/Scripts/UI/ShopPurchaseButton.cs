using BlueGravityTest.ScriptableObjects.Items;
using BlueGravityTest.Scripts.MVC;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BlueGravityTest.Scripts.UI
{
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
        private ItemData _itemData;
        public ItemData GetData() => _itemData;
        public void Initialize(ItemData data, int amountOfTheItem, PlayerModel player, ShopUiController shopUiController)
        {
            _itemData = data;
            itemIcon.sprite = data.itemIcon;
            _price = data.itemPurchasePrice;
            priceText.text = _price.ToString();
            _amountOfTheItem = amountOfTheItem;
            amountOfTheItemText.text = _amountOfTheItem.ToString();
            _player = player;
            _shopUiController = shopUiController;
            button.onClick.AddListener(PurchaseItem);
        }

        public void AddStack()
        {
            _amountOfTheItem++;
            amountOfTheItemText.text = _amountOfTheItem.ToString();
            button.interactable = true;
        }
        
        private void PurchaseItem()
        {
            if (!_player.GetPlayerWallet().Purchase(_price))
                return;
            _amountOfTheItem--;
            amountOfTheItemText.text = _amountOfTheItem.ToString();
            _shopUiController.SetCurrentPlayerGoldText();
            _player.GetPlayerInventory().AddItem(_itemData);
            if (_amountOfTheItem <= 0)
                button.interactable = false;
        }
    }
}

