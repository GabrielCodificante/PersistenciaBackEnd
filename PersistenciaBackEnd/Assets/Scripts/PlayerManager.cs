using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    [SerializeField] private int coins; 

    public int Coins
    {
        get => coins;
        set => coins = value;
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
    }
}
