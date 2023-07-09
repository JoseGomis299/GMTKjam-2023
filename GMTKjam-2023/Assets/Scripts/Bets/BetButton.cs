using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BetButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] private Color pressedColor;
    [SerializeField] private Color defaultColor;
    [SerializeField] private Image background;

    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text multiplier;

    private BetData _betData;

    private bool _clicked;
    private void OnEnable()
    {
        background.color = defaultColor;
    }

    public void Initialize(BetData betData)
    {
        gameObject.SetActive(true);
        _betData = betData;
        nameText.text = betData.name;
        multiplier.text = betData.multiplier.ToString();
    }
    

    public void OnPointerEnter(PointerEventData eventData)
    {
        background.color = pressedColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(_clicked) return;
        
        background.color = defaultColor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        BetManager.instance.ResetColorToAll();
        
        _clicked = true;
        background.color = pressedColor;
        
        BetManager.instance.SetCurrentBet(_betData);
    }

    public void ResetClick()
    {
        background.color = defaultColor;
        _clicked = false;
    }
}
