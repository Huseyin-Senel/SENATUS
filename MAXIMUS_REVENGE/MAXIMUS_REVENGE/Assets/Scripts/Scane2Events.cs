using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scane2Events : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] keys;
    public GameObject[] tetxs;
    public GameObject duvar1;
    public GameObject character;
    private bool run = true;


    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (run)
        {
            character.GetComponent<HeroKnight>().ScriptRun();
        }
        
    }


    public void controllerStart()
    {

        foreach (GameObject obj in keys)
        {
            obj.GetComponent<SpriteRenderer>().DOFade(1, 2);
        }

        foreach (GameObject obj in tetxs)
        {
            obj.GetComponent<TextMeshPro>().DOFade(1, 2);
        }
    }

    public void heroMove()
    {
        character.transform.DOLocalMoveX(-8.5f, 2);      
    }




    public void Second2Start()
    {
        StartCoroutine(Second2());
    }

    IEnumerator Second2()
    {
        yield return new WaitForSeconds(2);

        duvar1.GetComponent<BoxCollider2D>().enabled = true;
        character.GetComponent<HeroKnight>().active = true; 
        run = false;
    }

}
