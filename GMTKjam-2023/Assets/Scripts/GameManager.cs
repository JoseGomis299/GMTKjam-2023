using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject endGame;
    [SerializeField] private PauseState pauseMenu;
    private void Update()
    {
        if (MapManager.instance.aliveCharacters.Count <= 0)
        {
            Time.timeScale = 0;
            endGame.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
        }
    }
}
