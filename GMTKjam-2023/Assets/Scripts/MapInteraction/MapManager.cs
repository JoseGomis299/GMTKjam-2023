using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapManager : MonoBehaviour
{
    public List<Character> aliverCharacters { get; private set; }

    public int RoundNumber { get; set; } = 1;
    [SerializeField] private float mapRadius;
    
    public static MapManager instance { get; private set; }
    [SerializeField] private GameObject characterPrefab;

    private void Awake()
    {
        if(instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void GenerateCharacters(int characterCount)
    {
        aliverCharacters = new List<Character>();
        
        for (int i = 0; i < characterCount; i++)
        {
            aliverCharacters.Add(Instantiate(characterPrefab, GetCharacterSpawnPosition(), Quaternion.identity).GetComponent<Character>());
        }
    }

    private Vector3 GetCharacterSpawnPosition()
    {
        float angle = Random.Range(0f, 360f)*Mathf.Deg2Rad;
        float distance = Random.Range(-mapRadius, mapRadius);
        float xPos = transform.position.x + Mathf.Cos(angle) * distance;
        float yPos = transform.position.y + Mathf.Sin(angle) * distance;
        
        return new(xPos, yPos, transform.position.z - 50);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y, transform.position.z +1), mapRadius);
    }
}
