using System;
using System.Collections;
using System.Collections.Generic;
using ProjectUtils.Helpers;
using UnityEngine;

public class NextZoneManager : MonoBehaviour
{
    [SerializeField] private LayerMask mapLayer;
    [SerializeField] private SafeZone safeZone;
    [field:SerializeReference]  public float borderRadius { get; private set; }
    private bool _movingZone;
    private bool _editingZone;

    //Audio Manger related things
    //public AudioManager audioManager;
    public AudioClip selectNextZone;

    private void Start()
    {
        ShowNextZone();
        InvokeRepeating(nameof(NextZone), safeZone.idleZoneTime, safeZone.nextZoneTime+safeZone.idleZoneTime);
    }
    
    private void Update()
    {        
        if(!safeZone.nextZone.gameObject.activeInHierarchy || !_editingZone) return;

        if (Input.GetMouseButtonDown(0) && !Helpers.PointerIsOverButton())
        {
            _movingZone = !_movingZone;
            AudioManager.Instance.PlaySound(selectNextZone);
        }

        if (_movingZone)
        {       
            safeZone.nextZone.transform.position = GetMousePosition();
        }
    }

    private Vector3 GetMousePosition()
    {
        Vector3 mousePos = safeZone.nextZone.transform.position;
        if (Physics.Raycast(Helpers.Camera.ScreenPointToRay(Input.mousePosition), out var hit, 100,mapLayer))
        {
            mousePos = hit.point;
        }
        else return mousePos;

        if (Vector3.Distance(mousePos, safeZone.transform.position) + safeZone.nextZoneRadius*1.45f > safeZone.zoneRadius*1f)
        {
            Vector3 v = (mousePos - safeZone.transform.position).normalized;
            mousePos -= v * (Vector3.Distance(mousePos, safeZone.transform.position) + safeZone.nextZoneRadius*1.45f - safeZone.zoneRadius);
        }

        Vector3 pos = transform.position;
        pos.z = safeZone.transform.position.z;
        
        if(Vector3.Distance(mousePos,  pos) + safeZone.nextZoneRadius > borderRadius*1.5f)
        {
            Vector3 v = (mousePos -  pos).normalized;
            mousePos -= v * (Vector3.Distance(mousePos,  pos) + safeZone.nextZoneRadius - borderRadius*1.5f);
        }

        return mousePos;
    }
    
    private void NextZone()
    {
        GetComponent<DropManager>().AddDrop(1);
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
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.transform.position.y, transform.position.z), borderRadius);
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
