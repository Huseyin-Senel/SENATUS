using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSensor : MonoBehaviour
{
    public List<GameObject> objects = new List<GameObject>();



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!objects.Contains(collision.gameObject)) { objects.Add(collision.gameObject); }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        objects.Remove(collision.gameObject);
    }
}
