
using System.Collections.Generic;
using System.Linq;
using BlueGravityTest.ScriptableObjects.Items;
using BlueGravityTest.Scripts.Interactables;
using BlueGravityTest.Scripts.MVC;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace BlueGravityTest.Scripts.UI
{
    public class ShopUiController : MonoBehaviour
    {
        [SerializeField] private GameObject panel;
        [SerializeField] private GameObject sellPanel;
        [SerializeField] private GameObject purchasePanel;
        [SerializeField] private Transform contentSellPanel;
        [SerializeField] private Transform contentPurchasePanel;
        [SerializeField] private Button closeButton;
        [SerializeField] private Button purchaseButton;
        [SerializeField] private Button sellButton;
        [SerializeField] private ShopPurchaseButton shopPurchaseButtonPrefab;
        [SerializeField] private ShopSellButton shopSellButtonPrefab;
        [SerializeField] private TMP_Text currentGoldOfPLayer;

        private PlayerModel _player;
        private List<ShopPurchaseButton> _purchaseButtonsCreated = new();
        private List<ShopSellButton> _sellButtonsCreated = new();
        private bool _initialize;
        
        private void Awake()
        {
            Cursor.visible = false;
            closeButton.onClick.AddListener(delegate { OpenCloseShop(false); });
            purchaseButton.interactable = false;
            purchaseButton.onClick.AddListener(OpenPurchasePanel);
            sellButton.onClick.AddListener(OpenSellPanel);
        }

        public void SubscribeEvents(ShopController shopController)
        {
            shopController.InitializeStoreEvent += SpawnShopButton;
            shopController.OpenCloseShopEvent += OpenCloseShop;
        }

        private void SpawnShopButton(ShopData shopData, PlayerModel player)
        {
            currentGoldOfPLayer.text = player.GetPlayerWallet().GetCurrentGold().ToString();
            if (_initialize)
                return;
            _player = player;
            for (int i = 0; i < shopData.ItemsToSell.Count; i++)
            {
                var newButton = Instantiate(shopPurchaseButtonPrefab, contentPurchasePanel);
                newButton.Initialize(shopData.ItemsToSell[i].ItemToSell, shopData.ItemsToSell[i].AmountOfTheItem,
                    _player, this);
                _purchaseButtonsCreated.Add(newButton);
            }

            _initialize = true;
        }

        public void SetCurrentPlayerGoldText()
        {
            currentGoldOfPLayer.text = _player.GetPlayerWallet().GetCurrentGold().ToString();
        }

        private void OpenCloseShop(bool state)
        {
            _player.CanMove(!state);
            panel.SetActive(state);
            Cursor.visible = state;
        }

        public void AddNewPurchaseButton(ItemData data)
        {
            var duplicateButton = _purchaseButtonsCreated.FirstOrDefault(x => x.GetData() == data);
            if (duplicateButton == default)
            {
                var newButton = Instantiate(shopPurchaseButtonPrefab, contentPurchasePanel);
                newButton.Initialize(data, 1, _player, this);
                _purchaseButtonsCreated.Add(newButton);
                return;
            }
            
            duplicateButton.AddStack();
        }
        
        private void OpenSellPanel()
        {
            sellPanel.SetActive(true);
            purchasePanel.SetActive(false);
            sellButton.interactable = false;
            purchaseButton.interactable = true;
            for (int i = 0; i < _sellButtonsCreated.Count; i++)
                Destroy(_sellButtonsCreated[i].gameObject);
            _sellButtonsCreated.Clear();
            foreach (var itemToSell in _player.GetPlayerInventory().GetItems())
            {
                var newButton = Instantiate(shopSellButtonPrefab, contentSellPanel);
                newButton.Initialize(itemToSell,_player,this);
                _sellButtonsCreated.Add(newButton);
            }
        }

        private void OpenPurchasePanel()
        {
            sellPanel.SetActive(false);
            purchasePanel.SetActive(true);
            sellButton.interactable = true;
            purchaseButton.interactable = false;
        }
    }
}
