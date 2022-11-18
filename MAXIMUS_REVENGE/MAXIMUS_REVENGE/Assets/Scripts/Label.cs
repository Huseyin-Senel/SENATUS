using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Label : MonoBehaviour
{

    private TextMeshPro tmp;
    private Tween twText;
    private GameObject child0;
    private GameObject child1;

    public string str;
    public GameObject character;
    public GameObject triggerObject;
    


    // Start is called before the first frame update
    void Start()
    {
        //tmp = gameObject.GetComponentInChildren<TextMeshPro>();
        //grandChild = this.gameObject.transform.GetChild(0).GetChild(0).gameObject;
        child0 = gameObject.transform.GetChild(0).gameObject;
        child1 = gameObject.transform.GetChild(1).gameObject;
        tmp = child0.transform.GetChild(0).GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == character.name)
        {            
            child0.SetActive(true);
            child1.SetActive(true);

            typeText(tmp,"Collesium");

            if (triggerObject != null)
            {
                triggerObject.GetComponent<Collesium>().OnTriggerEnter2D(collision);
            }
        }
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == character.name)
        {
            child0.SetActive(false);
            child1.SetActive(false);

            if (triggerObject != null)
            {
                triggerObject.GetComponent<Collesium>().OnTriggerExit2D(collision);
            }
        }
    }


    private void typeText(TextMeshPro tmp ,string str,float typeSpeed = 15f)
    {
        string text = "";
        twText = DOTween.To(() => text, x => text = x, str, str.Length / typeSpeed).OnUpdate(() =>
        {
            tmp.text = text;
        });
    }

}
