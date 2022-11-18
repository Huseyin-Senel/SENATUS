using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIEvents : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
