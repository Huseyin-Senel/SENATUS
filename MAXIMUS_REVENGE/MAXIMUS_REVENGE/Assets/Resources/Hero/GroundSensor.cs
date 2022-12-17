using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSensor : MonoBehaviour
{
    public bool grounded = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "ground" || collision.gameObject.tag == "enemy")
        {
            //Debug.Log(gameObject.transform.parent.name + "  " + "true");
            grounded = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            //Debug.Log(gameObject.transform.parent.name + "  " + "false");
            grounded = false;
        }
    }
}
