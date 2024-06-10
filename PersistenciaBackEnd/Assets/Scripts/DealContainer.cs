using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DealContainer : MonoBehaviour
{
   [SerializeField] private Text playerID;
   [SerializeField] private Text date;
   [SerializeField] private Text itemName;
   [SerializeField] private Text balance;
   [SerializeField] private Text itemPrice;

   public void SetInformation(int playerID,string date,string itemName,int itemPrice,int balance){
        Debug.Log(playerID);
        Debug.Log(date);
        Debug.Log(itemName);
        Debug.Log(itemPrice);
        Debug.Log(balance);
        this.playerID.text = string.Format("ID: {0}", playerID);
        this.date.text = date;   
        this.itemName.text = itemName;
        this.itemPrice.text =  string.Format("C$ {0}", itemPrice);
        this.balance.text = string.Format("C$ {0}", balance);
   }
}
