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

    public void UpdateCredits(int c){
        IEnumerator setCredits = NetworkController.GetCredits(c, GameManager.instance.UserID);
        StartCoroutine(setCredits);
    }

    void Awake(){
        instance = this;
    }
}
