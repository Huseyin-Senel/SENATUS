using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    //250
    public void run(string count)
    {
        GetComponent<TextMeshProUGUI>().text = count;
        transform.DOLocalMove(new Vector2(transform.localPosition.x,250f),1f);
        GetComponent<TextMeshProUGUI>().DOFade(0,1f);


        this.Wait(1.1f,() => { Destroy(this.gameObject); });

    }
}
