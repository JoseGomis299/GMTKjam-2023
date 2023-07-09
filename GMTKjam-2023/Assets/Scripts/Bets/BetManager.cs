using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetManager : MonoBehaviour
{
    public static BetManager instance { get; private set; }
    private int _balance;

    public event Action<int> OnAddBalance;
    
    private void Awake()
    {
        if(instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void AddBalance(int amount)
    {
        _balance += amount;
        OnAddBalance?.Invoke(_balance);
    }

    public int GetBalance() => _balance;
}
