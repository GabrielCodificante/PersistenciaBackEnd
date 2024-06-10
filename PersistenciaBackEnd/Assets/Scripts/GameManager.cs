using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private int userID;
    [SerializeField] private string userName;
    [SerializeField] private int credits;
    [SerializeField] private int dealID;

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

      public int DealID{
        get => dealID;
        set => dealID = value;
    }

    void Awake(){
        instance = this;
    }
}
