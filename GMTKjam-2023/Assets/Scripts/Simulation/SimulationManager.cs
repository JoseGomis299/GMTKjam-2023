using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationManager : MonoBehaviour
{
    [SerializeField] private int playerCount;
    List<GameObject> playerVisualList;

    private bool _paused;
    public static SimulationManager instance { get; private set; }

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        GeneratePlayers();    
    }

    void GeneratePlayers()
    {
        MapManager.instance.GenerateCharacters(playerCount);
        foreach (Character player in MapManager.instance.aliverCharacters)
        {
            player.GetComponent<CharacterStateController>().SetDirectionMoving(new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)));
            player.GetComponent<CharacterStateController>().SetRandomTimeTarget(Random.Range(0.5f, 1.0f));
        }
    }

    private void Update()
    {
        if(_paused) return;
        
        foreach(Character player in MapManager.instance.aliverCharacters)
        {
            Vector2 direction = player.GetComponent<CharacterStateController>().GetDirectionMoving();

            player.transform.position += new Vector3(
                direction.x * Time.deltaTime * 0.01f,
                direction.y * Time.deltaTime * 0.01f,
                0);

            if (player.GetComponent<CharacterStateController>().GetTimeInDirection() >= player.GetComponent<CharacterStateController>().GetRandomTimeTarget())
            {
                print("HERE");
                player.GetComponent<CharacterStateController>().SetDirectionMoving(new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)));
                player.GetComponent<CharacterStateController>().SetRandomTimeTarget(Random.Range(0.5f, 1.0f));
                player.GetComponent<CharacterStateController>().ResetTimeInDirection();
            }

        }
    }

    public void pauseSimulation()
    {
        _paused = true;
    }
    
    public void ResumeSimulation()
    {
        _paused = false;
    }
}
