using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapManager : MonoBehaviour
{
    public List<Character> aliverCharacters { get; private set; }
    public List<Chest> chests { get; private set; }

    [field:SerializeReference] public float mapRadius { get; private set; }
    
    public static MapManager instance { get; private set; }
    [SerializeField] private GameObject characterPrefab;
    [SerializeField] private GameObject chestPrefab;

    [SerializeField] private Transform playersParent;
    [SerializeField] private Transform chestsParent;
    
    public int RoundNumber;

    private void Awake()
    {
        if(instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void GenerateCharacters(int characterCount)
    {
        aliverCharacters = new List<Character>();
        chests = new List<Chest>();
        for (int i = 0; i < characterCount; i++)
        {
            aliverCharacters.Add(Instantiate(characterPrefab, GetCharacterSpawnPosition(), Quaternion.Euler(0, 180, 0), playersParent).GetComponent<Character>());
            chests.Add(Instantiate(chestPrefab, GetCharacterSpawnPosition(), Quaternion.identity, chestsParent).GetComponent<Chest>());
        }
    }

    private Vector3 GetCharacterSpawnPosition()
    {
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        float distance = Random.Range(-mapRadius, mapRadius);
        float xPos = transform.position.x + Mathf.Cos(angle) * distance;
        float yPos = transform.position.y + Mathf.Sin(angle) * distance;

        return new(xPos, yPos, transform.position.z-0.15f);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y, transform.position.z), mapRadius);
    }
}
