using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slider : MonoBehaviour
{

    public UnityEngine.UI.Slider mSlider;
    public UnityEngine.UI.Button mButton;
    public Sprite off;
    public Sprite on;


    void Start()
    {
        mSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void imageClick()
    {
        if (mSlider.value == 0) 
        {
            mSlider.value = 100;
        }
        else
        {
            mSlider.value = 0;
        }
    }

    private void ValueChangeCheck()
    {
        if (mSlider.value==0)
        {
            mButton.image.sprite = off;
        }
        else
        {
            mButton.image.sprite = on;
        }
    }


}
