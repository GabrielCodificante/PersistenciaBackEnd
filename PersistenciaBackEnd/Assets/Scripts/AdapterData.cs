using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdapterData 
{
    /*Player Adapter*/
    public static void GetPlayer(PlayerData data, ref CharacterMovement player)
    {
        player.transform.position = data.position;
        player.transform.rotation = data.rotation;
    }
    public  static PlayerData GetPlayerData(CharacterMovement player )
    {
        PlayerData data = new PlayerData();
        data.position = player.transform.position;
        data.rotation = player.transform.rotation;
        return data;
    }

    /*Block Adapter*/
    public static void GetBlock(BlockData data, ref Block block)
    {
        block.transform.position = data.position;
        block.transform.rotation = data.rotation;
        block.transform.localScale = data.scale;
    }

    public static BlockData GetBlockData(Block block)
    {
        BlockData data = new BlockData();
        data.position = block.transform.position;
        data.rotation = block.transform.rotation;
        data.scale = block.transform.localScale;
        return data;
    }

    /*Coin Adapter*/
     public static void GetCoin(PlayerData data, ref Coin coin)
    {
        coin.transform.position = data.position;
    }
    public  static CoinData GetCoin(Coin coin )
    {
        CoinData data = new CoinData();
        data.position = coin.transform.position;
        return data;
    }
}

#region Class Datas
/*E mais facil de administrar todas as class datas dentro do mesmo arquivo*/
[Serializable]
public class PlayerData
{
    public Vector3 position;
    public Quaternion rotation;
    public int score;
}
[Serializable]
public class BlockData
{
    public Vector3 position;
    public Vector3 scale;
    public Quaternion rotation;
}
[Serializable]
public class CoinData
{
    public Vector3 position;
}

[Serializable]
public class SceneData
{
    public PlayerData player;
    public BlockData[] blocks;
    public CoinData [] coins;
}
#endregion
