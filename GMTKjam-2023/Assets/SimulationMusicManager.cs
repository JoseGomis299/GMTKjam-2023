using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationMusicManager : MonoBehaviour
{
    //Audio Manger related things
    public AudioManager audioManager;
    public AudioClip tensionMusic, ambient;

    public int playerNumberThreshold = 10;
    private bool hasTriggeredMusic = false;

    // Update is called once per frame
    void Update()
    {

        if (MapManager.instance.aliverCharacters.Count <= playerNumberThreshold && !hasTriggeredMusic)
        {
            audioManager.ChangeMusic(tensionMusic);
            hasTriggeredMusic = true;
        } 

        if (MapManager.instance.aliverCharacters.Count == 0 && hasTriggeredMusic)
        {
            audioManager.ChangeMusic(ambient);
        }


    }
}
