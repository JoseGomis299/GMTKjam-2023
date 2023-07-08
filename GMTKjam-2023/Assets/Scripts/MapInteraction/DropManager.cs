using System;
using System.Collections;
using System.Collections.Generic;
using ProjectUtils.Helpers;
using UnityEngine;

public class DropManager : MonoBehaviour
{
    private bool _dropping;
    [SerializeField] private GameObject dropPrefab;
    [SerializeField] private Transform map;

    private NextZoneManager _nextZoneManager;

    private void Start()
    {
       _nextZoneManager = GetComponent<NextZoneManager>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _dropping && !Helpers.PointerIsOverButton())
        {
            Vector3 mousePos = GetMousePosition();
            if(mousePos == Vector3.zero) return;
            
            Instantiate(dropPrefab, GetMousePosition(), Quaternion.identity, map);
        }
    }
    

    public void ToggleDropping()
    {
        _dropping = !_dropping;
    }

    public void DisableDropping()
    {
        _dropping = false;
    }

    private Vector3 GetMousePosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.x -= Screen.width / 2f;
        mousePos.y -= Screen.height / 2f;
        
        if(Vector3.Distance(mousePos,  transform.position) > _nextZoneManager.borderRadius*50) return Vector3.zero;
        return mousePos;
    }
}
