using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static UnityEditor.Progress;
using DG.Tweening;
using System.Data;

public class Envanter : MonoBehaviour
{

    public GameObject shiled, armor, bow, sword;
    public TextMeshProUGUI info, potText, armorText, attackText, moneyText;

    private int Carmor = 0;
    private int Cattack = 0;

    private int money = 1000;
    private int potCount= 0;


    public int Money { get => money; set => money = value; }

    void Start()
    {
        armorText.text = Carmor.ToString();
        attackText.text = Cattack.ToString();
        moneyText.text = money.ToString();
        potText.text = potCount.ToString();

    }



    public void buyItem(GameObject itemObj,GameObject slot)
    {
        Item item = itemObj.GetComponent<Item>();

        if (item.Tur == Item.Types.Pot)
        {
            if (money - item.Fiyat >= 0)
            {
                potCount++;
                potText.text = potCount.ToString();
                money -= item.Fiyat;
                moneyText.text = money.ToString();
                item.Fiyat = item.Fiyat / 2;


                item.changeSlot(slot);
                slot.GetComponent<Slot>().setItem(null);
                Destroy(itemObj);

            }
            else
            {
                infoSetText("yetersiz para");
            }
        }
        else
        {
            if (money - item.Fiyat >= 0)
            {
                money -= item.Fiyat;
                moneyText.text = money.ToString();
                item.Fiyat = item.Fiyat / 2;

                if (item.Tur == Item.Types.Shield || item.Tur == Item.Types.Armor)
                {
                    Carmor += item.Power;
                    armorText.text = Carmor.ToString();
                }
                else if (item.Tur == Item.Types.Bow || item.Tur == Item.Types.Sword)
                {
                    Cattack += item.Power;
                    attackText.text = Cattack.ToString();
                }

                item.changeSlot(slot);
            }
            else
            {
                infoSetText("yetersiz para");
            }
        }

    }


    public void sellItem(GameObject itemObj,GameObject slot)
    {
        Item item = itemObj.GetComponent<Item>();

        if (item.Tur != Item.Types.Pot)
        {
            money += item.Fiyat;
            moneyText.text = money.ToString();

            if (item.Tur == Item.Types.Shield || item.Tur == Item.Types.Armor)
            {
                Carmor -= item.Power;
                armorText.text = Carmor.ToString();
            }
            else if (item.Tur == Item.Types.Bow || item.Tur == Item.Types.Sword)
            {
                Cattack -= item.Power;
                attackText.text = Cattack.ToString();
            }

            item.changeSlot(slot);
            item.Fiyat = item.Fiyat * 2;

        }     
    }


    public void infoSetText(string text)
    {
        info.text = text;
        this.Wait(1f, () => { info.DOFade(0f, 1f); });

        this.Wait(2f, () => { info.text = ""; info.DOFade(1f, 0.1f); });
    }
}
