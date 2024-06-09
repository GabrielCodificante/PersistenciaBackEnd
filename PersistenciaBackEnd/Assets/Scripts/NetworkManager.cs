using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager instance;

    [Header ("Register")]
    [SerializeField] InputField txtName;
    [SerializeField] InputField txtEmail; 
    [SerializeField] InputField txtPassword;

    [Header ("Login")]
    [SerializeField] InputField txtEmail_L;
    [SerializeField] InputField txtPassword_L;

    public void Registar(){
        IEnumerator send = NetworkController.SendData(txtName.text, txtEmail.text, txtPassword.text);
        StartCoroutine(send);
    }

    public void Logar(){
        IEnumerator login = NetworkController.Login(txtEmail_L.text, txtPassword_L.text);
        StartCoroutine(login);
        
    }

    public void PegarNome(){
        IEnumerator setName = NetworkController.GetName(GameManager.instance.UserID);
        StartCoroutine(setName);
    }

    public void UpdateCredits(){
        IEnumerator setCredits = NetworkController.GetCredits(0, GameManager.instance.UserID);
        StartCoroutine(setCredits);
    }

    public void Buy(int itemID){
        IEnumerator buyItem = NetworkController.Buy(GameManager.instance.UserID, itemID);
        StartCoroutine(buyItem);
    }

    public void AddCredits(){
        IEnumerator setCredits = NetworkController.GetCredits(UIController.instance.CardCredits, GameManager.instance.UserID);
        StartCoroutine(setCredits);
    }

    void Awake(){
        instance = this;
    }
}
