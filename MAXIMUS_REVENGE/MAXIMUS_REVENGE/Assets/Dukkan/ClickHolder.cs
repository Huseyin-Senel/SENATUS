using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClickHolder : MonoBehaviour
{
    // Start is called before the first frame update

    public Image itemImage;
    public Image powerImage;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI powerStr;
    public TextMeshProUGUI fiyat;


    public Sprite powerImageDef;
    public Sprite powerImageAtk;
    public Sprite powerImagePot;


    public void SetItem(GameObject item1)
    {
        Item item = item1.GetComponent<Item>();

        switch (item.Tur)
        {
            case Item.Types.Sword:
                this.powerImage.sprite = powerImageAtk;
                break;
            case Item.Types.Bow:
                this.powerImage.sprite = powerImageAtk;
                break;
            case Item.Types.Pot:
                this.powerImage.sprite = powerImagePot;
                break;
            case Item.Types.Shield:
                this.powerImage.sprite = powerImageDef;
                break;
            case Item.Types.Armor:
                this.powerImage.sprite = powerImageDef;
                break;
        }
            


        this.itemName.text = item.ItemName;
        this.powerStr.text = "+"+item.Power.ToString();
        this.itemImage.sprite = item.ItemImage;
        this.fiyat.text = item.Fiyat.ToString();
    }
}
