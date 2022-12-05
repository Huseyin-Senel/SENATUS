using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enviorment : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("Kat 0")]
    [SerializeField] public List<GameObject> Up0 = new List<GameObject>();
    [SerializeField] public List<GameObject> Down0 = new List<GameObject>();
    [Header("Kat 1")]
    [SerializeField] public List<GameObject> Up1 = new List<GameObject>();
    [SerializeField] public List<GameObject> Down1 = new List<GameObject>();
    [Header("Kat 2")]
    [SerializeField] public List<GameObject> Up2 = new List<GameObject>();
    [SerializeField] public List<GameObject> Down2 = new List<GameObject>();
    [Header("Kat 3")]
    [SerializeField] public List<GameObject> Up3 = new List<GameObject>();
    [SerializeField] public List<GameObject> Down3 = new List<GameObject>();
    [Header("Kat 4")]
    [SerializeField] public List<GameObject> Up4 = new List<GameObject>();
    [SerializeField] public List<GameObject> Down4 = new List<GameObject>();


    [SerializeField] public List<GameObject> allCharacters = new List<GameObject>();


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
