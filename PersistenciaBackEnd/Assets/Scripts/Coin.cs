using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Coin : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            PlayerManager.instance.Coins++;
            this.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        this.gameObject.transform.position.y = Mathf.PingPong(0.5f,2);
    }
}
