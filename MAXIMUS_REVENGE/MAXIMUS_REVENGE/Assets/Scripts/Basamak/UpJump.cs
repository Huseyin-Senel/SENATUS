using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpJump : MonoBehaviour
{


    public bool usteCikarirMi = true;
    private bool right;
    // Start is called before the first frame update
    void Start()
    {
        if(this.name == "SagUpJump")
        {
            right = false;
        }
        else
        {
            right = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            if (collision.gameObject.GetComponent<Bandit>().jumpTarget == this.gameObject)
            {
                //collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, collision.gameObject.GetComponent<Rigidbody2D>().velocity.y);

                //collision.gameObject.GetComponent<Bandit>().AIjump(right,true);

                collision.gameObject.GetComponent<Bandit>().right = right;
            }
        }
    }



}
