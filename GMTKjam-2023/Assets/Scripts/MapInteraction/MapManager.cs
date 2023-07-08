using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapManager : MonoBehaviour
{
    public List<Character> aliverCharacters { get; private set; }

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
        for (int i = 0; i < characterCount; i++)
        {
            aliverCharacters.Add(Instantiate(characterPrefab, GetCharacterSpawnPosition(), Quaternion.identity).GetComponent<Character>());
        }
    }

    private Vector3 GetCharacterSpawnPosition() => new(Random.Range(transform.position.x - mapRadius, transform.position.x + mapRadius),
            Random.Range(transform.position.y - mapRadius, transform.position.y + mapRadius), transform.position.z);


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y, transform.position.z +1), mapRadius);
    }
}
