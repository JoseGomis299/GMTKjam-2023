using System;
using System.Collections;
using ProjectUtils.Helpers;
using UnityEngine;

public class SafeZone : MonoBehaviour
{
    [Header("Radius")]
    [SerializeField] private float zoneRadius;
    [SerializeField] private float reductionFactor;

    [Header("Timer")]
    [SerializeField] private float nextZoneTime;
    [SerializeField] private float idleZoneTime;
    [SerializeField] private int nextZoneCount;

    [Header("Damaging")]
    [SerializeField] private int damage;
    [SerializeField] private float attackSpeed;

    [Header("Next Zone")]
    [SerializeField] private Transform nextZone;
    [SerializeField] private float nextZoneRadius;
    public bool editingZone;
    
    private void Start()
    {
        transform.localScale = Vector3.one*zoneRadius;
        nextZoneRadius = zoneRadius*reductionFactor;
        nextZone.transform.localScale = nextZoneRadius * Vector3.one;

        InvokeRepeating(nameof(NextZone), idleZoneTime, nextZoneTime+idleZoneTime);
    }
    
    public void StartStorm()
    {
        InvokeRepeating(nameof(DealDamage), 0, attackSpeed);
        transform.localScale = Vector3.one*zoneRadius;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0)) editingZone = !editingZone;

        if (editingZone)
        {
            nextZone.transform.position = GetMousePosition();
        }
    }

    private Vector3 GetMousePosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.x -= Screen.width/2f;
        mousePos.y -= Screen.height/2f;

        if (Vector3.Distance(mousePos, transform.position) + nextZoneRadius*50 > zoneRadius*55)
        {
            Vector3 v = (mousePos - transform.position).normalized;
            mousePos -= v * ((Vector3.Distance(mousePos, transform.position) + nextZoneRadius * 50) - zoneRadius * 60);
        }

        return mousePos;
    }

    private void DealDamage()
    {
        foreach (var character in MapManager.instance.aliverCharacters)
        {
            if(!IsInZone(character.transform.position)) character.ReceiveTrueDamage(damage);
        }
    }

    private void NextZone()
    {
        if(nextZoneCount-- <= 0) return;
        MapManager.instance.RoundNumber++;

        nextZone.gameObject.SetActive(false);
        editingZone = true;

        StartCoroutine(ScaleZone(Vector3.one * nextZoneRadius, nextZoneTime));
        transform.DoMove(nextZone.transform.position,  nextZoneTime, ProjectUtils.Helpers.Transitions.TimeScales.Scaled);
    }

    private IEnumerator ScaleZone(Vector3 targetScale, float time)
    {
        float timer = Time.deltaTime;
        Vector3 initialScale = transform.localScale;
        Vector3 scaleDelta = targetScale - initialScale;

        while (timer < time)
        {
            transform.localScale = initialScale + scaleDelta * (timer/time);
            zoneRadius = transform.localScale.x;
            yield return null;
            timer += Time.deltaTime;
        }

        transform.localScale = targetScale;
        zoneRadius = transform.localScale.x;

        //Modify nextZone size
        if (nextZoneCount > 0)
        {
            nextZone.gameObject.SetActive(true);
            nextZoneRadius *= reductionFactor;
            nextZone.transform.localScale = nextZoneRadius * Vector3.one;
        }
    }

    private bool IsInZone(Vector3 position) => Mathf.Abs(position.x) <= transform.position.x + zoneRadius
                                               && Mathf.Abs(position.y) <= transform.position.y + zoneRadius;
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y, transform.position.z +1), zoneRadius*50);
    }
}
