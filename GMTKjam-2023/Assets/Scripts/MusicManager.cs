using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{

    private Scene currentScene;
    private int currentSceneIndex;
    
    //Second Scene Specific Variables
    public bool lastStorm = false;

    //Music Variables
    //private bool changeMusic = false;
    private int currentMusic = 0; // 0 -> Menu, 1 -> Ambient, 2 -> Tension

    //Audio Manger related things
    public AudioManager audioManager;
    public AudioClip menu, ambient, tension;


    // Start is called before the first frame update
    void Start()
    {
        //Get scene
        currentScene = SceneManager.GetActiveScene();
        currentSceneIndex = currentScene.buildIndex;

        //Get Storm Situation if Scene is Room
        if (currentSceneIndex == 1)
        {
            //Check if storm is last storm
            if (lastStorm)
            {
                currentMusic = 2;
            } else
            {
                currentMusic = 1;
            }
        } else
        {
            currentMusic = 0;
        }

        //Begin playing music corresponding to current scene

        switch (currentMusic)
        {
            case 0:
                {
                    audioManager.ChangeMusic(menu);
                    break;
                }
            case 1:
                {
                    audioManager.ChangeMusic(ambient);
                    break;
                }
            case 2:
                {
                    audioManager.ChangeMusic(tension);
                    break;
                }
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        
        //Get Scene


        //If scene is room, get which phase of circle closing we are in


        //On scene change, change music to corresponding one



    }
}
