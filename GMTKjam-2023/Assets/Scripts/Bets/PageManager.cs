using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PageManager : MonoBehaviour
{
  [SerializeField] private Button nextPageButton;
  [SerializeField] private Button prevPageButton;
  [SerializeField] private TMP_Text text;

  private int _currentPage;
  private void Start()
  {
     
    nextPageButton.onClick.AddListener(() =>
    {
        int numPages = Mathf.CeilToInt(MapManager.instance.aliveCharacters.Count / 20f);
        if (_currentPage+1 < numPages)
        {
            BetManager.instance.GenerateBetData(MapManager.instance.aliveCharacters, ++_currentPage);
            text.text = _currentPage.ToString();
        }

        if (_currentPage >= numPages-1) nextPageButton.interactable = false;
        prevPageButton.interactable = true;
    });
    
    if (_currentPage <= 0) prevPageButton.interactable = false;
    
    prevPageButton.onClick.AddListener(() =>
    { 
        if (_currentPage-1 >= 0)
        {
            BetManager.instance.GenerateBetData(MapManager.instance.aliveCharacters, --_currentPage);
            text.text = _currentPage.ToString();
        }

        if (_currentPage <= 0) prevPageButton.interactable = false;
        nextPageButton.interactable = true;
    });
  }
}
