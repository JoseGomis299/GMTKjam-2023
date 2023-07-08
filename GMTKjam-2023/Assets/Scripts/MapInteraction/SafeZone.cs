using System;
using System.Collections;
using ProjectUtils.Helpers;
using UnityEngine;

public class SafeZone : MonoBehaviour
{
    [field: Header("Radius")]
    [field:SerializeReference]  public float zoneRadius { get; private set; }
    [SerializeField] private float reductionFactor;

    [Header("Timer")]
    [SerializeField] private int nextZoneCount;
    [field:SerializeReference] public float nextZoneTime { get; private set; }
    [field:SerializeReference] public float idleZoneTime { get; private set; }

    [Header("Damaging")]
    [SerializeField] private int damage;
    [SerializeField] private float attackSpeed;

    [field: Header("Next Zone")]
    [field:SerializeReference] public Transform nextZone { get; private set; }
    [field:SerializeReference] public float nextZoneRadius { get; private set; }

    private NextZoneManager _nextZoneManager;

    
    private void Start()
    {
        _nextZoneManager = MapManager.instance.GetComponent<NextZoneManager>();
        
        transform.localScale = Vector3.one*zoneRadius;
        nextZoneRadius = zoneRadius*reductionFactor;
        nextZone.transform.localScale = nextZoneRadius * Vector3.one;
        
        InvokeRepeating(nameof(DealDamage), 0, attackSpeed);
    }
    
    private void DealDamage()
    {
        foreach (var character in MapManager.instance.aliverCharacters)
        {
            if(!IsInZone(character.transform.position)) character.ReceiveTrueDamage(damage);
        }
    }

    public void NextZone()
    {
        if(nextZoneCount-- <= 0) return;
        MapManager.instance.RoundNumber++;

        _nextZoneManager.EnableMoving();

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
            nextZone.gameObject.SetActive(false);
            zoneRadius = transform.localScale.x;
            yield return null;
            timer += Time.deltaTime;
        }

        transform.localScale = targetScale;
        zoneRadius = transform.localScale.x;

        //Modify nextZone size
        if (nextZoneCount > 0)
        {
            _nextZoneManager.ShowNextZone();
            nextZoneRadius *= reductionFactor;
            nextZone.transform.localScale = nextZoneRadius * Vector3.one;
        }
    }

    private bool IsInZone(Vector3 position) => Vector3.Distance(position, transform.position) <= zoneRadius*50;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y, transform.position.z +1), zoneRadius*50);
    }
}
