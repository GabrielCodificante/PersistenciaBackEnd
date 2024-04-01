using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    [SerializeField] public Text coinsTxt;
    [SerializeField] private int coins; 


    public int Coins
    {
        get => coins;
        set 
        {
            coins = value;
            coinsTxt.text = string.Format("{0} / {1}",coins,SaveLoadController.instance.GetCoinsLength);
        } 
    }

    public void LoadPrefs()
    {
        coins = PlayerPrefs.GetInt("coins");
         coinsTxt.text = string.Format("{0} / {1}",coins,SaveLoadController.instance.GetCoinsLength);
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
    }
}
