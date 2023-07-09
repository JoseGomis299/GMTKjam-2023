using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{


    public int gameStartScene;

    //Audio Manger related things
    public AudioManager audioManager;
    public AudioClip ambient, genericGood, genericBad;

    public void StartGame()
    {
        audioManager.PlaySound(genericGood);
        audioManager.ChangeMusic(ambient);
        SceneManager.LoadScene(gameStartScene);
    }

    public void QuitGame()
    {
        audioManager.PlaySound(genericBad);
        Application.Quit();
    }
}
