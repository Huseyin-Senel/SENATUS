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


    public bool active = false;

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
            active = true;


            child0.SetActive(true);
            child1.SetActive(true);
            typeText(tmp,str);

            if (triggerObject != null)
            {
                scaleUp(triggerObject);
            }
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == character.name)
        {
            active = false;


            child0.SetActive(false);
            child1.SetActive(false);

            if (triggerObject != null)
            {
                scaleDown(triggerObject);
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




    public void scaleUp(GameObject gameObj)
    {
        gameObj.transform.DOScale(new Vector3(1f, 1f, 1f), 1);
        gameObj.transform.DOLocalMoveY(-4.65f, 1);
        gameObj.GetComponent<SpriteRenderer>().DOColor(new Color(1f, 1f, 1f), 1);
    }

    public void scaleDown(GameObject gameObj)
    {
        gameObj.transform.DOScale(new Vector3(0.9f, 0.9f, 0.9f), 1);
        gameObj.transform.DOLocalMoveY(-5f, 1);
        gameObj.GetComponent<SpriteRenderer>().DOColor(new Color(0.627f, 0.627f, 0.627f), 1);
    }

}
