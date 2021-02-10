using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public int LifePotionsAmount = 0;
    public int Coins = 0;
    public Text lifePotionsText;
    public Text coinsText;

  

    void Update()
    {
        lifePotionsText.text = "x" + LifePotionsAmount;
    }

    public void EarnMoney(int money)
    {
        Coins += money;
    }

    public void SpendMoney(int money)
    {
        Coins -= money;
    }

    public void AddLifePotion()
    {
        LifePotionsAmount++;
    }

    public void SpendLifePotion() {
        LifePotionsAmount--;
    }

}
