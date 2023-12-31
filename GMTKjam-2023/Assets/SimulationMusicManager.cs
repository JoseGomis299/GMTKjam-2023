using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationMusicManager : MonoBehaviour
{
    //Audio Manger related things
    //public AudioManager audioManager;
    public AudioClip tensionMusic, ambient;

    public int playerNumberThreshold = 10;
    private bool hasTriggeredMusic = false;

    // Update is called once per frame
    void Update()
    {

        if (MapManager.instance.aliveCharacters.Count <= playerNumberThreshold && !hasTriggeredMusic)
        {
            AudioManager.Instance.ChangeMusic(tensionMusic);
            hasTriggeredMusic = true;
        } 

        if (MapManager.instance.aliveCharacters.Count == 0 && hasTriggeredMusic)
        {
            AudioManager.Instance.ChangeMusic(ambient);
        }


    }
}
