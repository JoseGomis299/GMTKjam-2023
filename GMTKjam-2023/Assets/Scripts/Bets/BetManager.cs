using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BetManager : MonoBehaviour
{
    public static BetManager instance { get; private set; }
    private int _balance;

    private BetData _currentBet;
    private BetData _placedBet;
    private Character _betCharacter;
    private int _bidAmount;

    [SerializeField] private PlayerDataUI playerDataUI;
    [SerializeField] private Button placeBid;
    [SerializeField] private Button closeButton;
    [SerializeField] private TMP_InputField bidAmount;
    [SerializeField] private TMP_Text balance;
    [SerializeField] private TMP_Text balanceExtern;
    [SerializeField] private Transform rows;
    private List<BetButton> _buttons = new List<BetButton>();
    private List<BetData> _betData = new List<BetData>();

    private Color _playerDefaultColor;
    [SerializeField] private Color betPlayerColor;

    //Audio Manger related things
    //public AudioManager audioManager;
    public AudioClip computerOpen, computerClose, computerInteract;

    private void Awake()
    {
        if(instance == null) instance = this;
        else Destroy(gameObject);
    }
    
    private void Start()
    {
        _buttons = new List<BetButton>();
        AddBalance(100);
        
        foreach (Transform row in rows)
        {
            foreach (Transform betButton in row)
            {
                _buttons.Add(betButton.GetComponent<BetButton>());
            }
        }
        
        placeBid.onClick.AddListener(() =>
        {
            _bidAmount = int.Parse(bidAmount.text);
            bidAmount.text = "";
            _balance -= _bidAmount;
            balance.text = "BALANCE: $"+_balance;
            balanceExtern.text = $"${_balance}";
            
            _placedBet = _currentBet;
            _playerDefaultColor = _betCharacter.GetComponent<SpriteRenderer>().color;
            _betCharacter.GetComponent<SpriteRenderer>().color = betPlayerColor;
            playerDataUI.Initialize(_betCharacter.GetCharacterData(), _bidAmount);
            placeBid.interactable = false;
        });
        
        bidAmount.onEndEdit.AddListener((t) =>
        {
            if (int.Parse(t) > 0 && _currentBet != null) placeBid.interactable = true;
            else placeBid.interactable = false;
        });
        
        closeButton.onClick.AddListener(() =>
        {
            transform.GetChild(0).gameObject.SetActive(false);
        });
    }

    private void OnEnable()
    {
        if (_placedBet == null)
        {
            playerDataUI.gameObject.SetActive(false);
            ResetColorToAll();
        }
        placeBid.interactable = false;
    }

    public void GenerateBetData(List<Character> characters, int page)
    {
        _betData = new List<BetData>();
        
        foreach (var character in characters)
        {
            _betData.Add(new BetData(character.GetCharacterData()));
        }
        
        _betData.Sort((x,y)=> (int) (y.multiplier*100) - (int) (x.multiplier*100));

        foreach (var button in _buttons)
        {
            button.gameObject.SetActive(false);
        }

        for (int i = 0; i < _buttons.Count; i++)
        {
            if(i+_buttons.Count*page >=  _betData.Count) return;
            
            _buttons[i].Initialize(_betData[i+_buttons.Count*page]);
        }
    }

    public void AddBalance(float amount)
    {
        _balance += (int) amount;
        balance.text = "BALANCE: $"+_balance;
        balanceExtern.text = $"${_balance}";
    }

    public void PayBet()
    {
        if(_placedBet == null || MapManager.instance.aliverCharacters.Find(x => x.name == _placedBet.name) == null) return;
        
        AddBalance(_bidAmount*_placedBet.multiplier);
        _placedBet = null;
        _bidAmount = 0;
        playerDataUI.Initialize(_betCharacter.GetCharacterData(), _bidAmount);
        ResetColorToAll();

        foreach (var character in MapManager.instance.aliverCharacters)
        {
            character.GetComponent<SpriteRenderer>().color = _playerDefaultColor;
        }
    }

    public void ResetColorToAll()
    {
        if (_placedBet == null && bidAmount.text != "" && int.Parse(bidAmount.text) > 0) placeBid.interactable = true;
        else placeBid.interactable = false;
        
        foreach (var button in _buttons)
        {
            button.ResetClick();
        }
    }

    public int GetBalance() => _balance;

    public void SetCurrentBet(BetData betData)
    {
        _currentBet = betData;
        if (_placedBet == null || _currentBet == _placedBet)
        {
            playerDataUI.gameObject.SetActive(true);
            _betCharacter = MapManager.instance.aliverCharacters.Find(x => x.name == betData.name);
            playerDataUI.Initialize(_betCharacter.GetCharacterData(), _bidAmount);
        }
    }

    //Sonido del Ordenador
    public void PlayOnSound()
    {
        AudioManager.Instance.PlaySound(computerOpen);
    }

    public void PlayOffSound()
    {
        AudioManager.Instance.PlaySound(computerClose);
    }

    public void PlayInteractSound()
    {
        AudioManager.Instance.PlaySound(computerInteract);
    }
}
