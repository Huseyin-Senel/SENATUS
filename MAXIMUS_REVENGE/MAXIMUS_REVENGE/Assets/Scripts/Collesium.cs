using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor;

public class Collesium: MonoBehaviour
{


    public GameObject Character;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == Character.name)
        {
            Color color = new Color(1, 1, 1);
            gameObject.GetComponent<SpriteRenderer>().color = color;
            gameObject.transform.DOScale(new Vector3(1f, 1f, 1f), 1);
            gameObject.transform.DOLocalMoveY(-4.65f, 1);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == Character.name)
        {
            Color color = new Color(160f / 255f, 160f / 255f, 160f / 255f);
            gameObject.GetComponent<SpriteRenderer>().color = color;
            gameObject.transform.DOScale(new Vector3(0.9f, 0.9f, 0.9f), 1);
            gameObject.transform.DOLocalMoveY(-5f, 1);
        }
    }
}
