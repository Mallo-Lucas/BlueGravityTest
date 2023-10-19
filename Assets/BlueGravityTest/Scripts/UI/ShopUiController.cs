using System;
using System.Collections;
using System.Collections.Generic;
using BlueGravityTest.ScriptableObjects.Items;
using BlueGravityTest.Scripts.Interactables;
using BlueGravityTest.Scripts.MVC;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ShopUiController : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Transform contentPanel;
    [SerializeField] private Button closeButton;
    [SerializeField] private ShopPurchaseButton shopPurchaseButtonPrefab;
    [SerializeField] private TMP_Text currentGoldOfPLayer;

    private PlayerModel _player;
    private bool _initialize;
    
    
    
    private void Awake()
    {
        Cursor.visible = false;
        closeButton.onClick.AddListener(delegate { OpenCloseShop(false); });
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
            var newButton = Instantiate(shopPurchaseButtonPrefab,contentPanel);
            newButton.Initialize(shopData.ItemsToSell[i].ItemToSell, shopData.ItemsToSell[i].AmountOfTheItem, _player, this);
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
}
