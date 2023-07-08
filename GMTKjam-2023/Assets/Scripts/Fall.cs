using System.Collections;
using System.Collections.Generic;
using ProjectUtils.Helpers;
using UnityEngine;

public class Fall : MonoBehaviour
{
    [SerializeField] private float fallTime;

    private void OnEnable()
    {
        StartCoroutine(DoFall());
    }

    private IEnumerator DoFall()
    {
        Chest chest = GetComponent<Chest>();
        
        if(chest != null) chest.enabled = false;
        SimulationManager.instance.pauseSimulation();

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color color = spriteRenderer.color;
        spriteRenderer.color = new Color(color.r * 2f, color.g * 2f, color.b * 2f);
        spriteRenderer.DoChangeColor(color, fallTime, ProjectUtils.Helpers.Transitions.TimeScales.Scaled);
        
        transform.localScale *= 2f;
        yield return transform.DoScale(transform.localScale/1.5f, fallTime, ProjectUtils.Helpers.Transitions.TimeScales.Scaled);
        
        if(chest != null) chest.enabled = true;
        SimulationManager.instance.ResumeSimulation();
    }
}
