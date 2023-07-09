using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapButtonManager : MonoBehaviour
{
    [SerializeField] private Button editZoneButton;
    [SerializeField] private Button dropsButton;

    private NextZoneManager _nextZoneManager;
    private DropManager _dropManager;

    //Audio Manger related things
    //public AudioManager audioManager;
    public AudioClip button;


    private void Start()
    {
        _nextZoneManager = MapManager.instance.GetComponent<NextZoneManager>();
        _dropManager = MapManager.instance.GetComponent<DropManager>();

        editZoneButton.onClick.AddListener(() =>
        {   
            _nextZoneManager.ToggleEditing();
            _dropManager.DisableDropping();
        });
        
        dropsButton.onClick.AddListener(() =>
        {
            _dropManager.ToggleDropping();
            _nextZoneManager.DisableEditing();
        });
    }

    public void PlayButtonSound() 
    {
        AudioManager.Instance.PlaySound(button);

    }
}
