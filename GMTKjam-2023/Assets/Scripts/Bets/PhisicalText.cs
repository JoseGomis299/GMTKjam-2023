using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PhisicalText : MonoBehaviour
{
    [SerializeField] private TextAsset text;
    [SerializeField] private GameObject textSheet;

    public AudioClip paper;
    
    private void Start()
    {
       
    }
    
    private void OnMouseDown()
    {
        

        AudioManager.Instance.PlaySound(paper);
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