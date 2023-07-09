using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Weapon startWeapon;
    
    private int _health;
    private int _shield;

    private int _luck;
    private int _aim;
    private int _evasion;

    private Weapon _currentWeapon;
    private List<ConsumableItem> _itemInventory;
    
    public CharacterData GetCharacterData() => new CharacterData(name, _health, _shield, _luck, _aim, _evasion, _currentWeapon, _itemInventory);


    private void Awake()
    {
        InitializeStats();
    }
    
    private void InitializeStats()
    {
        _health = 100;
        _shield = 0;

        _luck = Random.Range(-100, 101);
        _aim = Random.Range(-100, 101);
        _evasion = Random.Range(-100, 101);

        _currentWeapon = startWeapon;
        _itemInventory = new List<ConsumableItem>();
        name = NamePicker.PickName();
    }

    public Weapon GetWeapon()
    {
        return _currentWeapon;
    }

    public void ReceiveDamage(int damage)
    {
        _shield -= damage;
        if (_shield >= 0) return;
        
        _health += _shield;
        _shield = 0;
        
        if(_health < 0) Die();
    }
    
    public void ReceiveTrueDamage(int damage)
    {
        _health -= damage;
        if(_health < 0) Die();
    }

    public float GetHealth()
    {
        return _health;
    }

    public float GetShield()
    {
        return _shield;
    }
    
    private void Die()
    {
        MapManager.instance.aliverCharacters.Remove(this);
    }

    public void GrabWeapon(Weapon weapon)
    {
        _currentWeapon = weapon;
    }
    
    public void GrabItem(ConsumableItem item)
    {
        _itemInventory.Add(item);
    }
    
    public void ConsumeItem(ConsumableItem item)
    {
        _health += item.healthRegeneration;
        _shield += item.shieldRegeneration;
        if (_health > 100) _health = 100;
        if (_shield > 100) _shield = 100;
       
        _itemInventory.Remove(item);
    }
    
}
