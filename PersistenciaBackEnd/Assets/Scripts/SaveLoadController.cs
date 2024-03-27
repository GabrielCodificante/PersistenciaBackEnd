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
    public string path;

    #endregion

    void Start()
    {
        if(instance!=null){
            Destroy(this);
        }
        else
        {
            instance=this;
            DontDestroyOnLoad(this.gameObject);
        } 

        path= Application.persistentDataPath +"save.json";
    }


    void Update()
    {
        
    }
}
