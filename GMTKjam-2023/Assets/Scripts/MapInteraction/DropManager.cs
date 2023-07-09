using System;
using System.Collections;
using System.Collections.Generic;
using ProjectUtils.Helpers;
using TMPro;
using UnityEngine;

public class DropManager : MonoBehaviour
{
    [SerializeField] private LayerMask mapLayer;
    private bool _dropping;
    private int _dropCount;
    
    [SerializeField] private GameObject dropPrefab;
    [SerializeField] private Transform parent;

    private NextZoneManager _nextZoneManager;
    [SerializeField] private TMP_Text dropCountText;

    //Audio Manger related things
    public AudioClip drop;

    private void Start()
    {
       _nextZoneManager = GetComponent<NextZoneManager>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _dropping && !Helpers.PointerIsOverButton() && _dropCount-- > 0)
        {
            Vector3 mousePos = GetMousePosition();
            if(mousePos == Vector3.zero) return;

            AudioManager.Instance.PlaySound(drop);
            
            Instantiate(dropPrefab, GetMousePosition(), Quaternion.identity, parent);
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
        Vector3 mousePos = Vector3.zero;
        if (Physics.Raycast(Helpers.Camera.ScreenPointToRay(Input.mousePosition), out var hit, 100,mapLayer))
        {
            mousePos = hit.point;
        }
        else return mousePos;
        
        if(Vector3.Distance(mousePos,  transform.position) > _nextZoneManager.borderRadius) return Vector3.zero;
        return mousePos;
    }

    public void AddDrop(int n)
    {
        _dropCount += n;
        dropCountText.text = _dropCount.ToString();
    }
}
