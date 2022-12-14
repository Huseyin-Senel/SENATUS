using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class InfoText : MonoBehaviour
{
    // Start is called before the first frame update



    public TextMeshPro tmp;

    public enum Type
    {
        green,
        yellow,
        red
    }


    public void run(string text, InfoText.Type type)
    {
        switch (type)
        {
            case InfoText.Type.green:
                gameObject.transform.DOLocalMove(new Vector2(transform.localPosition.x-0.6f,transform.localPosition.y+0.6f),1f);
                tmp.text = text;
                tmp.color = new Color(0f,1f,0f,1f);
                tmp.DOFade(0f,1f);
                this.Wait(1.1f, () =>
                {
                    Destroy(this.gameObject);
                });

                break;
            case InfoText.Type.yellow:
                gameObject.transform.DOLocalMove(new Vector2(transform.localPosition.x + 0f, transform.localPosition.y + 0.6f), 1f);
                tmp.text = text;
                tmp.color = new Color(1f, 1f, 0f, 1f);
                tmp.DOFade(0f, 1f);
                this.Wait(1.1f, () =>
                {
                    Destroy(this.gameObject);
                });
                break;
            case InfoText.Type.red:
                gameObject.transform.DOLocalMove(new Vector2(transform.localPosition.x+0.6f, transform.localPosition.y+ 0.6f), 1f);
                tmp.text = text;
                tmp.color = new Color(1f, 0f, 0f, 1f);
                tmp.DOFade(0f, 1f);
                this.Wait(1.1f, () =>
                {
                    Destroy(this.gameObject);
                });
                break;
        }
    }
}
