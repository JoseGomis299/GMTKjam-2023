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
    }

    private void Update()
    {
        if(_paused) return;
        
        foreach(Character player in MapManager.instance.aliverCharacters)
        {
            player.transform.position += new Vector3(Time.deltaTime*0.01f, 0f, 0f);
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
