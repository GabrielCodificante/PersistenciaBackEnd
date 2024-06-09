using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ItemLoadController : MonoBehaviour
{
    GameObject lojaPanel;
    ItemContainer prefab;

    public IEnumerator LoadItem(){
        string url = "site";
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success){
            Debug.Log(request.error);
        }else{
            string json = request.downloadHandler.text;
            ItemData itemData = JsonUtility.FromJson<ItemData>(json);
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
        public string price;
        public string score;
    }
}


