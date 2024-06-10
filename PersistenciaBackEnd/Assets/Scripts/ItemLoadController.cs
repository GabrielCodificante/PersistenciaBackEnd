using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ItemLoadController : MonoBehaviour
{
    public static ItemLoadController instance;
    [SerializeField] private GameObject lojaPanel;
    public ItemContainer prefab;

    public IEnumerator LoadItem(){
        string url = "https://projetobackendunity.000webhostapp.com/loja_projeto/carditem_api.php/";
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success){
            Debug.Log(request.error);
        }else{
            string json = request.downloadHandler.text;
            Debug.Log(json);
            ItemData itemData = JsonUtility.FromJson<ItemData>(json);
            Debug.Log(itemData.status);
            foreach(Item item in itemData.data){
                ItemContainer itemContainer = Instantiate<ItemContainer>(prefab, transform.position, Quaternion.identity, lojaPanel.transform);
                itemContainer.SetInformation(item.id, item.name, item.price, item.amount, item.image);
            }
        }
    }

    public IEnumerator UpdateStock(ItemContainer container){
        string url = "https://projetobackendunity.000webhostapp.com/loja_projeto/carditem_api.php/";
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success){
            Debug.Log(request.error);
        }else{
            string json = request.downloadHandler.text;
            Debug.Log(json);
            ItemData itemData = JsonUtility.FromJson<ItemData>(json);
            Debug.Log(itemData.status);
            foreach(Item item in itemData.data){
                if(container.productID == item.id){
                    container.SetStock(item.amount);
                }
            }
        }
    }

    public void LoadContainer(ItemContainer container){
        IEnumerator loadContainer = UpdateStock(container);
        StartCoroutine(loadContainer);
    }

    [Serializable]
    public class ItemData
    {
        public string status;
        public Item[] data;
    }

    [Serializable]
    public class Item
    {
        public int id;
        public string name;
        public int price;
        public int amount;
        public string image;
    }

    void Awake(){
        instance = this;
    }

    void Start(){
        StartCoroutine("LoadItem");
    }
}


