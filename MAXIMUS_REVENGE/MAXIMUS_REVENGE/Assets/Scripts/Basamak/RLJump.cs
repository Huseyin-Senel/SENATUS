using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RLJump : MonoBehaviour
{
    private bool right;

    void Start()
    {
        if (this.name == "SagJump")
        {
            right = true;
        }
        else
        {
            right = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            if (collision.gameObject.GetComponent<Bandit>().satirdaKal)
            {

                collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, collision.gameObject.GetComponent<Rigidbody2D>().velocity.y);

                collision.gameObject.GetComponent<Bandit>().AIjump(right, false);
            }
        }
    }

}
