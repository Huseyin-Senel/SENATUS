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
    public Vector3 triggerObjectPos;
    [SerializeField] public float scaleFactorMin;
    [SerializeField] public float scaleFactorMax;


    public bool characterIn = false;

    // Start is called before the first frame update
    void Start()
    {
        //tmp = gameObject.GetComponentInChildren<TextMeshPro>();
        //grandChild = this.gameObject.transform.GetChild(0).GetChild(0).gameObject;
        child0 = gameObject.transform.GetChild(0).gameObject;
        child1 = gameObject.transform.GetChild(1).gameObject;
        tmp = child0.transform.GetChild(0).GetComponent<TextMeshPro>();


        if (triggerObject != null)
        {
            triggerObjectPos = triggerObject.transform.localPosition;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == character.name)
        {    
            characterIn = true;


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
            characterIn = false;


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
        gameObj.transform.DOScale(new Vector3(scaleFactorMax, scaleFactorMax, scaleFactorMax), 1);
        gameObj.transform.DOLocalMoveY(triggerObjectPos.y+0.4f, 1);
        gameObj.GetComponent<SpriteRenderer>().DOColor(new Color(1f, 1f, 1f), 1);
    }

    public void scaleDown(GameObject gameObj)
    {
        gameObj.transform.DOScale(new Vector3(scaleFactorMin, scaleFactorMin, scaleFactorMin), 1);
        gameObj.transform.DOLocalMoveY(triggerObjectPos.y, 1);
        gameObj.GetComponent<SpriteRenderer>().DOColor(new Color(0.627f, 0.627f, 0.627f), 1);
    }

}
