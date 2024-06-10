using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    [Header("Panels")]
    [SerializeField] private GameObject loginPanel;
    [SerializeField] private GameObject registerPanel;
    [SerializeField] private GameObject storePanel;
    [SerializeField] private GameObject feedbackPanel;
    [SerializeField] private GameObject failPanel;

    [Header("UserInfo")]
    [SerializeField] private Text userName;
    [SerializeField] private Text userCredits;
    [SerializeField] private InputField cardCredits;

    public GameObject GetLoginPanel{
        get => loginPanel;
    }

    public GameObject GetRegisterPanel{
        get => registerPanel;
    }

    public GameObject GetStorePanel{
        get => storePanel;
    }

    public GameObject GetFeedbackPanel{
        get => feedbackPanel;
    }

    public GameObject GetFailPanel{
        get => failPanel;
    }

    public string UserName{
        set => userName.text = string.Format("Bem vindo, {0}!", value);
    }

    public string UserCredits{
        set => userCredits.text = value;
    }

    public int CardCredits{
        get => int.Parse(cardCredits.text);
    }
    
    void Awake(){
        instance = this;
    }
}
