using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laptop : MonoBehaviour
{
    private void OnMouseEnter()
    {
        GetComponent<Outline>().enabled = true;
    }
    
    private void OnMouseExit()
    {
        GetComponent<Outline>().enabled = false;
    }
}
