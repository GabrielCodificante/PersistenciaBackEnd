using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    [SerializeField] Text coinsTxt;
    [SerializeField] private int coins; 


    public int Coins
    {
        get => coins;
        set 
        {
            coins = value;
            coinsTxt.text = string.Format("{0} / {1}",coins,coins); //coins.ToString() + " | 15";
        } 
    }

    public void LoadPrefs()
    {
        coins = PlayerPrefs.GetInt("coins");
    }

    public void SavePrefs()
    {
        PlayerPrefs.SetInt("coins", coins);
    }

    void Awake()
    {
        if(instance == null){
            instance = this;
        }
        coinsTxt.text = string.Format("{0} / {1}",coins,coins);
    }
}
