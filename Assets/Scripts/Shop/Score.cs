using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class Score : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void AddCoinsExtern();

    public GameObject advButton;
    public GameController gameController;


    public void ShowAdvButton() 
    { 
        AddCoinsExtern();
        advButton.SetActive(false);
    }


    public void AddCoins()
    {
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + CarController.countCars * 2);
        gameController.coinsCount.text = PlayerPrefs.GetInt("Coins").ToString();
    }
}
