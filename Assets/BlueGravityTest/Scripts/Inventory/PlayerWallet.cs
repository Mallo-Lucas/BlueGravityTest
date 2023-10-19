using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlueGravityTest.Scripts.Inventory
{
    public class PlayerWallet
    {
        private int _currentGold;

        public int GetCurrentGold() => _currentGold;
   
        public PlayerWallet(int defaultAmountOfGold)
        {
            _currentGold = defaultAmountOfGold;
        }

        public bool Purchase(int amount)
        {
            if (_currentGold< amount)
                return false;

            _currentGold -= amount;
            return true;
        }

        public void Sell(int amount)
        {
            _currentGold += amount;
        }
    }
}

