using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationManager : MonoBehaviour
{
    [SerializeField] private int playerCount;
    List<GameObject> playerVisualList;

    private void Start()
    {
        GeneratePlayers();    
    }

    void GeneratePlayers()
    {
        MapManager.instance.GenerateCharacters(playerCount);
    }

    private void Update()
    {
        foreach(Character player in MapManager.instance.aliverCharacters)
        {
            player.transform.position += new Vector3(Time.deltaTime*100f, 0f, 0f);
        }
    }
}
