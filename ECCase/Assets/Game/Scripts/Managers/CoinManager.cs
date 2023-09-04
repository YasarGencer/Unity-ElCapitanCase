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
    }
    public void Spend(int value) {
        coins -= value;
        PlayerPrefs.SetInt("Coins", coins);
    }

    public int GetCoin() {
        return coins;
    }
}
