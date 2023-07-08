using System;
using System.Collections;
using System.Collections.Generic;
using ProjectUtils.Helpers;
using UnityEngine;

public class NextZoneManager : MonoBehaviour
{
    [SerializeField] private SafeZone safeZone;
    [field:SerializeReference]  public float borderRadius { get; private set; }
    private bool _movingZone;
    private bool _editingZone;


    private void Start()
    {
        ShowNextZone();
        InvokeRepeating(nameof(NextZone), safeZone.idleZoneTime, safeZone.nextZoneTime+safeZone.idleZoneTime);
    }
    
    private void Update()
    {        
        if(!safeZone.nextZone.gameObject.activeInHierarchy || !_editingZone) return;
        
        if(Input.GetMouseButtonDown(0) && !Helpers.PointerIsOverButton()) _movingZone = !_movingZone;

        if (_movingZone)
        {
            safeZone.nextZone.transform.position = GetMousePosition();
        }
    }

    private Vector3 GetMousePosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.x -= Screen.width/2f;
        mousePos.y -= Screen.height/2f;

        if (Vector3.Distance(mousePos, safeZone.transform.position) + safeZone.nextZoneRadius*50 > safeZone.zoneRadius*70)
        {
            Vector3 v = (mousePos - safeZone.transform.position).normalized;
            mousePos -= v * (Vector3.Distance(mousePos, safeZone.transform.position) + safeZone.nextZoneRadius * 50 - safeZone.zoneRadius * 70);
        }
        
        if(Vector3.Distance(mousePos,  transform.position) + safeZone.nextZoneRadius*50 > borderRadius*50)
        {
            Vector3 v = (mousePos -  transform.position).normalized;
            mousePos -= v * (Vector3.Distance(mousePos,  transform.position) + safeZone.nextZoneRadius * 50 -   borderRadius*50);
        }

        return mousePos;
    }
    
    private void NextZone()
    {
       safeZone.NextZone();
    }

    public void ToggleEditing()
    {
        _editingZone = !_editingZone;
        safeZone.nextZone.gameObject.SetActive(_editingZone);
        EnableMoving();
    }
    
    public void DisableEditing()
    {
        _editingZone = false;
        safeZone.nextZone.gameObject.SetActive(false);
        EnableMoving();
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.transform.position.y, transform.position.z +1), borderRadius*50);
    }

    public void EnableMoving()
    {
        _movingZone = _editingZone;
    }

    public void ShowNextZone()
    {
        safeZone.nextZone.gameObject.SetActive(_editingZone);
    }
}
