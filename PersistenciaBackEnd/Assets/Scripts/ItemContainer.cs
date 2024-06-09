using UnityEngine;
using UnityEngine.UI;

public class ItemContainer : MonoBehaviour
{
    [SerializeField] private Image productImg;
    [SerializeField] private Text productName;
    [SerializeField] private Text productPrice;
    [SerializeField] private Text productStock;

    public void SetInformation(Image image, string name, string price, string stock){
        //productImg.sprite = ima;
        productName.text = name;
        productPrice.text = string.Format("C$ {0}", price);
        productStock.text = string.Format("N. em estoque: {0}", stock);
    }
}
