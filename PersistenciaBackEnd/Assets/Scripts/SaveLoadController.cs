using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveLoadController : MonoBehaviour
{
    #region Atributes
    public static SaveLoadController instance;

    public CharacterMovement player;

    public GameObject block, coin;
    private string path;
    public string pathName;

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

  
        List<Coin> coins = new List<Coin>();

        foreach(BlockData block in sceneData.blocks)
        {
            AdapterData.CreateBlock(this.block,block);
        }

        foreach(CoinData coin in sceneData.coins)
        {
            //Instantiate(this.coin,coin.position,Quaternion.identity);
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

        path = Application.persistentDataPath + "_" + pathName + ".json";
    }


  
}
