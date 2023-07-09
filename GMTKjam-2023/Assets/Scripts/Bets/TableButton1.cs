using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TableButton1 : MonoBehaviour
{
    private NextZoneManager _nextZoneManager;
    private DropManager _dropManager;

    [SerializeField] private bool isDrop;
    
    public AudioClip button;
    
    private void Start()
    {
        _nextZoneManager = MapManager.instance.GetComponent<NextZoneManager>();
        _dropManager = MapManager.instance.GetComponent<DropManager>();
    }
    
    private void OnMouseDown()
    {
        if (!isDrop)
        {
            _nextZoneManager.ToggleEditing();
            _dropManager.DisableDropping();
        }
        else
        {
            _dropManager.ToggleDropping();
            _nextZoneManager.DisableEditing();
        }

        AudioManager.Instance.PlaySound(button);
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