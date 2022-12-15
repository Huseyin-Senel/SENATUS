using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basamak : MonoBehaviour
{

    private PlatformEffector2D effector;
    public float waitTime;
    [SerializeField] public float timeOfset = 0.5f;


    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.S))
        {
            waitTime = timeOfset;
        }

        if (Input.GetKey(KeyCode.S))
        {
            if(waitTime <= 0)
            {
                effector.rotationalOffset = 180f;
                waitTime = timeOfset;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }

        if (Input.GetKey(KeyCode.W))
        {
            effector.rotationalOffset = 0;
        }


    }

}
