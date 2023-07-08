using System;
using System.Collections;
using System.Collections.Generic;
using ProjectUtils.Helpers;
using UnityEngine;

public class MapScreen : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private RenderTexture renderTexture;
    private void OnMouseDown()
    {        
        camera.targetTexture = null;
        Helpers.Camera.enabled = false;
        camera.enabled = true;
    }
}
