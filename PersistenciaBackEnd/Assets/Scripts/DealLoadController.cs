using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DealLoadController : MonoBehaviour
{    
    public static DealLoadController instance;
    [SerializeField] private GameObject dealPanel;
    public DealContainer prefab;


    public IEnumerator LoadDeal(int playerID){
        WWWForm form= new WWWForm();
        form.AddField("id",playerID);
        string url = "https://projetobackendunity.000webhostapp.com/loja_projeto/deal_api.php/";
        UnityWebRequest request = UnityWebRequest.Post(url,form);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success){
            Debug.Log(request.error);
        }else{
            string json = request.downloadHandler.text;
            Debug.Log(json);
            DealData dealData = JsonUtility.FromJson<DealData>(json);
            Debug.Log(dealData.status);
            foreach(Deal deal in dealData.data){
                if(deal.id_deal > GameManager.instance.DealID){

                    GameManager.instance.DealID=deal.id_deal;
                    DealContainer dealContainer = Instantiate<DealContainer>(prefab, transform.position, Quaternion.identity, dealPanel.transform);
                    dealContainer.SetInformation(deal.id_deal, deal.date, deal.name, deal.price, deal.balance);
                    
                }
            }
        }
    }

    [Serializable]
    public class DealData
    {
        public string status;
        public Deal[] data;
    }

    [Serializable]
    public class Deal
    {
        public int id;
        public string date;
        public int price;
        public string name;
        public int balance;
        public int id_deal;
    }

    public void CreateTable(){
        IEnumerator createTable = LoadDeal(GameManager.instance.UserID);
        StartCoroutine(createTable);
    }

    void Start(){
        instance = this;
    }
}