using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laptop : MonoBehaviour
{
    [SerializeField] private GameObject bets;
    private void OnMouseDown()
    {
        bets.transform.GetChild(0).gameObject.SetActive(true);
    }

    private void OnMouseEnter()
    {
        GetComponent<Outline>().enabled = true;
    }
    
    private void OnMouseExit()
    {
        GetComponent<Outline>().enabled = false;
    }
}
