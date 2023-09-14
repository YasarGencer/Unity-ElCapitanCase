using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    int coins;
    public void Initialize() {
        coins = PlayerPrefs.GetInt("Coins", 0);
    } 
    public void Gain(int value) {
        coins += value;
        PlayerPrefs.SetInt("Coins", coins);
        MainManager.Instance.EventManager.InvokeEvent(EventTypes.CurrencyEarned);
    }
    public bool Spend(int value) {
        if (coins < value)
            return false;
        coins -= value;
        PlayerPrefs.SetInt("Coins", coins);
        MainManager.Instance.EventManager.InvokeEvent(EventTypes.CurrencySpent);
        return true;
    }

    public int GetCoin() {
        return coins;
    }
}
