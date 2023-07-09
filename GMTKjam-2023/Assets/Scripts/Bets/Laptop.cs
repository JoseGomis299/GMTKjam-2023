using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laptop : MonoBehaviour
{


    //Audio Manger related things
    //public AudioManager audioManager;
    public AudioClip laptopOn;

    [SerializeField] private GameObject bets;
    private void OnMouseDown()
    {
        bets.transform.GetChild(0).gameObject.SetActive(true);
        AudioManager.Instance.PlaySound(laptopOn);
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
