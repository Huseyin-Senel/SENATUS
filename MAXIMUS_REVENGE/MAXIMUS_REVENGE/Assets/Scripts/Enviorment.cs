using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enviorment : MonoBehaviour
{

    [SerializeField] public List<GameObject> kat1 = new List<GameObject>();
    [SerializeField] public List<GameObject> kat2 = new List<GameObject>();
    [SerializeField] public List<GameObject> kat3 = new List<GameObject>();
    [SerializeField] public List<GameObject> kat4 = new List<GameObject>(); 

    [Header("----------")]

    public List<GameObject> Up0 = new List<GameObject>();
    public List<GameObject> Down0 = new List<GameObject>();
    public List<GameObject> Up1 = new List<GameObject>();
    public List<GameObject> Down1 = new List<GameObject>();
    public List<GameObject> Up2 = new List<GameObject>();
    public List<GameObject> Down2 = new List<GameObject>();
    public List<GameObject> Up3 = new List<GameObject>();
    public List<GameObject> Down3 = new List<GameObject>();
    public List<GameObject> Up4 = new List<GameObject>();
    public List<GameObject> Down4 = new List<GameObject>();

    public List<GameObject> allCharacters = new List<GameObject>();


    void Start()
    {

        change();

    }


    private void change()
    {
        foreach (GameObject obj in kat1)
        {
            add(obj,Up0,Down1);
        }
        foreach (GameObject obj in kat2)
        {
            add(obj, Up1, Down2);
        }
        foreach (GameObject obj in kat3)
        {
            add(obj, Up2, Down3);
        }
        foreach (GameObject obj in kat4)
        {
            add(obj, Up3, Down4);
        }
    }


    private void add(GameObject basamak, List<GameObject> up, List<GameObject> down)
    {
        if (basamak.transform.Find("SagUpJump").gameObject.activeSelf)
        {
            up.Add(basamak.transform.Find("SagUpJump").gameObject);
        }
        if (basamak.transform.Find("SolUpJump").gameObject.activeSelf)
        {
            up.Add(basamak.transform.Find("SolUpJump").gameObject);
        }


        if (basamak.transform.Find("SagDown").gameObject.activeSelf)
        {
            down.Add(basamak.transform.Find("SagDown").gameObject);
        }
        if (basamak.transform.Find("SolDown").gameObject.activeSelf)
        {
            down.Add(basamak.transform.Find("SolDown").gameObject);
        }
    }

}
