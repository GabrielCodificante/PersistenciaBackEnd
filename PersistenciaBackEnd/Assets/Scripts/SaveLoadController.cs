using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;

public class SaveLoadController : MonoBehaviour
{
    #region Atributes
    public static SaveLoadController instance;

    public CharacterMovement player;

    public GameObject block, coin;
    private int coinsLength;
    private string path;
    [SerializeField] private string url; 

    public int GetCoinsLength{
        get=>coinsLength;
    }
    #endregion

    #region Methods

    public void Save()
    {
        Block[] blocks = FindObjectsByType<Block>(FindObjectsSortMode.None);
        List<BlockData> blockDataList = new List<BlockData>();

        Coin[]  coins = FindObjectsByType<Coin>(FindObjectsSortMode.None);
        List<CoinData> coinDataList = new List<CoinData>();

        PlayerData playerData = AdapterData.GetPlayerData(player);

        foreach(Block block in blocks)
        {
            blockDataList.Add(AdapterData.GetBlockData(block));
        }

        foreach(Coin coin in coins)
        {
            coinDataList.Add(AdapterData.GetCoinData(coin));
        }


        SceneData sceneData = new SceneData();
        sceneData.player = playerData;
        sceneData.blocks = blockDataList.ToArray(); 
        sceneData.coins = coinDataList.ToArray();

        string json = JsonUtility.ToJson(sceneData);//Cria o Json        
        Debug.Log(json);
        File.WriteAllText(path,json);

    }

    public void Load()
    {
        string json;
        SceneData sceneData= new SceneData();

        Clear();

        json = File.ReadAllText(path);
        sceneData = JsonUtility.FromJson<SceneData>(json);
        coinsLength= sceneData.coins.Length;
        PlayerManager.instance.Coins=0;

        foreach(BlockData block in sceneData.blocks)
        {
            AdapterData.CreateBlock(this.block,block);
        }

        foreach(CoinData coin in sceneData.coins)
        {
            AdapterData.CreateCoin(this.coin,coin);
        }

        AdapterData.GetPlayer(sceneData.player, ref player);
        

    }

    public void GenerateWebScene(UnityWebRequest web)
    {   
            string json;
            SceneData sceneData= new SceneData();

            Clear();

            json = web.downloadHandler.text;
            sceneData = JsonUtility.FromJson<SceneData>(json);
            coinsLength= sceneData.coins.Length;
            PlayerManager.instance.Coins=0;

            foreach(BlockData block in sceneData.blocks)
            {
                AdapterData.CreateBlock(this.block,block);
            }

            foreach(CoinData coin in sceneData.coins)
            {
                AdapterData.CreateCoin(this.coin,coin);
            }

            AdapterData.GetPlayer(sceneData.player, ref player);
        

    }

    void Clear()
    {
        Block[] blocks = FindObjectsByType<Block>(FindObjectsSortMode.None);
        Coin[] coins = FindObjectsByType<Coin>(FindObjectsSortMode.None);

        foreach(Block block in blocks)
        {
            Destroy(block.gameObject);
        }

        foreach(Coin coin in coins)
        {
            Destroy(coin.gameObject);
        }
    }

    #endregion

    #region Coroutines

    public IEnumerator OnlineLoad(int num){
        UnityWebRequest webRequest = UnityWebRequest.Get(url + "nivel" + num.ToString() + ".json");
        yield return webRequest.SendWebRequest();

        if(webRequest.result == UnityWebRequest.Result.Success)
        {
           GenerateWebScene(webRequest);
        }
    }
    #endregion
    void Start()
    {
        if(instance != null){
            Destroy(this);
        }
        else
        {
            instance=this;
            DontDestroyOnLoad(this.gameObject);
        } 

        path = Application.persistentDataPath + "_" + "nivel.json";
    }


  
}
