using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIEvents : MonoBehaviour
{
    private bool shopOn = false;
    private bool collesiumOn = false;

    public Label label1, label2,label3;

    private HeroKnight characterScript;

    // Start is called before the first frame update
    void Start()
    {
        characterScript = GameObject.Find("HeroKnight").GetComponent<HeroKnight>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (shopOn)
            {
                GameObject a = GameObject.Find("SellBuy");
                a.transform.DOLocalMove(new Vector3(50,+1000,1),1f);
                shopOn = false;
                characterScript.active = true;
            }

            if (collesiumOn)
            {
                GameObject a = GameObject.Find("Collesium");
                a.transform.DOLocalMove(new Vector3(0, +1000, 1), 1f);
                collesiumOn = false;
                characterScript.active = true;
            }
        }


        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (label1.characterIn)
            {

            }
            if (label2.characterIn)
            {
                if (!collesiumOn)
                {
                    GameObject a = GameObject.Find("Collesium");
                    a.transform.DOLocalMove(new Vector3(0, -40, 1), 1f);
                    collesiumOn = true;
                    characterScript.active = false;
                }
            }
            if (label3.characterIn)
            {
                if (!shopOn)
                {
                    GameObject a = GameObject.Find("SellBuy");
                    a.transform.DOLocalMove(new Vector3(50, -40, 1), 1f);
                    shopOn = true;
                    characterScript.active = false;
                }
            }



            
        }
    }



    public void Play_Click()
    {
        GameObject menu = GameObject.Find("Menu");
        //menu.transform.DOMoveY(1500, 2);
        menu.transform.DOLocalMoveY(600,2);
        //menu.transform.DOMoveY(1000, 2).SetEase(Ease.OutBounce);


        GameObject background = GameObject.Find("Shadow");
        SpriteRenderer sr = background.GetComponent<SpriteRenderer>();
        sr.DOFade(0.3f, 2);
        //Color color = new Color(160f/255f,160f/255f,160f/255f);
        //sr.DOColor(color, 2);
        //sr.color = color;


        GameObject sound = GameObject.Find("Sound");
        sound.transform.DOMoveY(-100, 2);

    }
}
