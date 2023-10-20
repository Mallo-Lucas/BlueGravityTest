using System;
using BlueGravityTest.ScriptableObjects.Items;
using BlueGravityTest.Scripts.MVC;
using BlueGravityTest.Scripts.UI;
using UnityEngine;

namespace BlueGravityTest.Scripts.Interactables
{
    public class ShopController : Interactable
    {
        [SerializeField] private ShopData shopData;
        [SerializeField] private ShopUiController shopUiController;

        public Action<bool> OpenCloseShopEvent;
        public Action<ShopData, PlayerModel> InitializeStoreEvent;
        
        private PlayerModel _player;
        

        public override void Interact(PlayerModel model)
        {
            _player = model;
            InitializeStore();
            OpenShop();
        }

        private void OpenShop()
        {
            OpenCloseShopEvent?.Invoke(true);
        }

        private void InitializeStore()
        {
            shopUiController.SubscribeEvents(this);
            InitializeStoreEvent?.Invoke(shopData, _player);
        }
    }
}

