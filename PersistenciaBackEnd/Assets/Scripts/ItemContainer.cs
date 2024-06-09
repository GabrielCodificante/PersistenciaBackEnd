using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ItemContainer : MonoBehaviour
{
    [SerializeField] private int productID;
    [SerializeField] private Image productImg;
    [SerializeField] private Text productName;
    [SerializeField] private Text productPrice;
    [SerializeField] private Text productStock;
    [SerializeField] private string imageLink;

    public void SetInformation(int id, string name, int price, int stock, string link){
        productID = id;
        productName.text = name;
        productPrice.text = string.Format("C$ {0}", price);
        productStock.text = string.Format("N. em estoque: {0}", stock);
        imageLink = link;
        StartCoroutine("LoadImage");
    }

    public void BuyButton(){
        NetworkManager.instance.Buy(productID);
    }

    public IEnumerator LoadImage()
    {
      
        UnityWebRequest requestTexture = UnityWebRequestTexture.GetTexture(imageLink);
        yield return requestTexture.SendWebRequest();

        if (requestTexture.result == UnityWebRequest.Result.Success)
        {
            Texture2D tex= ((DownloadHandlerTexture) requestTexture.downloadHandler).texture;
            Rect rect= new Rect(0,0,tex.width,tex.height);
            Vector2 rectCenter = new Vector2(rect.width / 2.0f, rect.height / 2.0f);
            Sprite sprite = Sprite.Create(tex,rect,rectCenter);
            
            productImg.sprite = sprite;           
        }
    }

    /*private void UpdateImage(){
        IEnumerator upImg = NetworkController.LoadImage(imageLink, productImg.sprite);
        StartCoroutine(upImg);
    }*/

    void Start(){
       
    }
}
