using UnityEngine;
using System;
using TMPro;

public class BidAmountValidator : MonoBehaviour
{
    private void Start()
    {
        GetComponent<TMP_InputField>().onValidateInput += (input, charIndex, addedChar) => Validate(input, addedChar);
    }
    
    private char Validate(string text,  char ch)
    {
        if (ch >= '0' && ch <= '9')
        {
            text += ch;
            if(int.Parse(text) > BetManager.instance.GetBalance()) return (char)0;
            return ch;
        }

        return (char)0;
    }
}
    