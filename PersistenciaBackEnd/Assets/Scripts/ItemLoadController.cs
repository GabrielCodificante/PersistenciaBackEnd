using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ItemLoadController : MonoBehaviour
{
    [SerializeField] private GameObject lojaPanel;
    [SerializeField] private ItemContainer prefab;

    public IEnumerator LoadItem(){
        string url = "https://projetobackendunity.000webhostapp.com/loja_projeto/carditem_api.php/";
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success){
            Debug.Log(request.error);
        }else{
            string json = request.downloadHandler.text;
            ItemData itemData = JsonUtility.FromJson<ItemData>(json);
            Debug.Log(itemData.status);
            foreach(Item item in itemData.data){
                ItemContainer itemContainer = Instantiate<ItemContainer> (prefab, transform.position, Quaternion.identity, lojaPanel.transform);
            }
        }
    }

    [Serializable]
    public class ItemData
    {
        public string status;
        public Item[] data;
    }

    public class Item
    {
        public int id;
        public string name;
        public int price;
        public int score;
    }

    void Star(){
        StartCoroutine("LoadItem");
    }
}


