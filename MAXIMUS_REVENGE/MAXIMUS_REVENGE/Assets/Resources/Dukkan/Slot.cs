using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IDropHandler
{
    
    public bool characterSlot=false;
    [SerializeField] Item.Types tur;


    GameObject item = null;




    public void setItem(GameObject item)
    {
        this.item = item;    
    }

    public GameObject getItem() 
    {
        return item;
    }



    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null )
        {
            if (item == null)
            {
                if (characterSlot)
                {       //Buy
                    if (eventData.pointerDrag.GetComponent<Item>().Tur == tur)
                    {
                        GameObject.Find("Envanter").GetComponent<Envanter>().buyItem(eventData.pointerDrag, this.gameObject);                       
                    }
                }
                else if(eventData.pointerDrag.GetComponent<Item>().CurrentSlot.GetComponent<Slot>().characterSlot)
                {       //Sell
                    GameObject.Find("Envanter").GetComponent<Envanter>().sellItem(eventData.pointerDrag, this.gameObject);
                }
                else
                {       //dükkan yer deðiþtirme
                    eventData.pointerDrag.GetComponent<Item>().changeSlot(this.gameObject);
                }
            }
            else if(characterSlot)
            {
                GameObject.Find("Envanter").GetComponent<Envanter>().infoSetText("ýlk once takili olan itemi satin"); 
            }           
            
        }

    }
}
