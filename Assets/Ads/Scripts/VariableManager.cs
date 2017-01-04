using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VariableManager : MonoBehaviour {

    public int coins;                                       //Number of coins in the player's wallet.
    public bool fesiveSkin;                                 //Number of coins in the player's wallet.
    public int longestSurvived;                             //The longest number of days survived on record.
    public List<Theme> themes = new List<Theme>();          //Themes and their status

    //Adds coins to the wallet and saves the value to the device.
    public void AddCoins(int amount)
    {
        //Add coins to wallet.
        coins += amount;

        //Saves the wallet's current balance to the device .
        this.GetComponent<SaveAndLoad>().SaveCurrency();

        //Display the wallet's current coin balance.
        GameObject.FindGameObjectWithTag("CoinText").GetComponent<Text>().text = coins.ToString();
    }

    public void AssignThemeStates(Theme one, Theme two)
    {
        themes[0] = one;
        themes[1] = two;
    }
}