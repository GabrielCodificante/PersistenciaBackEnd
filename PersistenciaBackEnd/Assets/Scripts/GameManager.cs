using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private int userID;
    [SerializeField] private string userName;
    [SerializeField] private int credits;

    public int UserID{
        get => userID;
        set => userID = value;
    }

    public string UserName{
        get => userName;
        set {
            userName = value;
            UIController.instance.UserName = userName;
        } 
    }

    public int Credits{
        get => credits;
        set {
            credits = value;
            UIController.instance.UserCredits = Convert.ToString(credits);
        } 
    }

    void Awake(){
        instance = this;
    }
}
