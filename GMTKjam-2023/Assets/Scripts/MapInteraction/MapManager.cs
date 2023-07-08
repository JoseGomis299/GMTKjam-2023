using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapManager : MonoBehaviour
{
    public List<Character> aliverCharacters { get; private set; }

    [field:SerializeReference] public float mapRadius { get; private set; }
    
    public static MapManager instance { get; private set; }
    [SerializeField] private GameObject characterPrefab;
    [SerializeField] private GameObject chestPrefab;

    public int RoundNumber;

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
            aliverCharacters.Add(Instantiate(characterPrefab, GetCharacterSpawnPosition(), Quaternion.Euler(0, 180, 0)).GetComponent<Character>());
            Instantiate(chestPrefab, GetCharacterSpawnPosition(), Quaternion.identity);
        }
    }

    private Vector3 GetCharacterSpawnPosition()
    {
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
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
