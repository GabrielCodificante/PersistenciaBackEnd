using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class NetworkController
{
    public static IEnumerator SendData(string name, string email, string password){
        WWWForm form = new WWWForm();
        form.AddField("name", name);
        form.AddField("email", email);
        form.AddField("pass", password);
        UnityWebRequest request = UnityWebRequest.Post("https://projetobackendunity.000webhostapp.com/loja_projeto/register.php/", form);
        yield return request.SendWebRequest();

        //Debug 
        if(request.result != UnityWebRequest.Result.Success){
            Debug.Log(request.error);
        }else{
            Debug.Log("Usuário cadastrado!");
            UIController.instance.GetRegisterPanel.SetActive(false);
            UIController.instance.GetLoginPanel.SetActive(true);
        }
    }

    public static IEnumerator Login(string email, string password){
        WWWForm form = new WWWForm();
        form.AddField("email", email);
        form.AddField("pass", password);
        UnityWebRequest request = UnityWebRequest.Post("https://projetobackendunity.000webhostapp.com/loja_projeto/login.php/", form);
        yield return request.SendWebRequest();

        //Debug
        if(request.result != UnityWebRequest.Result.Success){
            Debug.Log(request.error);
        }else{
            string i = request.downloadHandler.text;
            Debug.Log("??? " + i);
            if(int.Parse(request.downloadHandler.text) > 0){
                Debug.Log("Bem vindo!");
                GameManager.instance.UserID = int.Parse(request.downloadHandler.text);
                NetworkManager.instance.PegarNome();
                NetworkManager.instance.UpdateCredits();
                yield return new WaitForSeconds(1);
                UIController.instance.GetLoginPanel.SetActive(false);
                UIController.instance.GetStorePanel.SetActive(true);
                DealLoadController.instance.CreateTable();
            }            
            Debug.Log(request.downloadHandler.text);
        }
    }

    public static IEnumerator GetName(int id){
        Debug.Log("ID passado: " + id);
        WWWForm form = new WWWForm();
        form.AddField("player_id", id);
        UnityWebRequest request = UnityWebRequest.Post("https://projetobackendunity.000webhostapp.com/loja_projeto/getplayername.php/", form);
        yield return request.SendWebRequest();

        //Debug
        if(request.result != UnityWebRequest.Result.Success){
            Debug.Log(request.error);
        }else{
            GameManager.instance.UserName = request.downloadHandler.text;
            Debug.Log(request.downloadHandler.text);
        }
    }

    public static IEnumerator GetCredits(int credits, int userID){
        WWWForm form = new WWWForm();
        form.AddField("credits", credits);
        form.AddField("player_id", userID);
        UnityWebRequest request = UnityWebRequest.Post("https://projetobackendunity.000webhostapp.com/loja_projeto/addcredits.php/", form);
        yield return request.SendWebRequest();

        //Debug
        if(request.result != UnityWebRequest.Result.Success){
            Debug.Log(request.error);
        }else{
            string i = request.downloadHandler.text;
            Debug.Log(i);
            //Debug.Log("CREDITS " + i);
            GameManager.instance.Credits = int.Parse(i);
        }
    }

    public static IEnumerator Buy(int playerID,int itemID){
        WWWForm form= new WWWForm();
        form.AddField("player_id",playerID);
        form.AddField("item_id",itemID);
        UnityWebRequest request = UnityWebRequest.Post("https://projetobackendunity.000webhostapp.com/loja_projeto/buy.php/",form);
        yield return request.SendWebRequest();

        //Debug
        if(request.result != UnityWebRequest.Result.Success){
            Debug.Log(request.error);
        }else{
            Debug.Log("Compra feita");
            Debug.Log(request.downloadHandler.text);
            if(int.Parse(request.downloadHandler.text) > 0){
                NetworkManager.instance.UpdateCredits();
                UIController.instance.GetFeedbackPanel.SetActive(true);
                DealLoadController.instance.CreateTable();
            }else{
                UIController.instance.GetFailPanel.SetActive(true);
            }
            
        }
    }
}
