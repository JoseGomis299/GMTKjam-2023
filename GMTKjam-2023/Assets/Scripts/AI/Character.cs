using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private int _health;
    private int _shield;

    private int _luck;
    private int _aim;
    private int _evasion;

    private Weapon _currentWeapon;
    private List<ConsumableItem> _itemInventory;
    
    public CharacterData GetCharacterData() => new CharacterData(name, _health, _shield, _luck, _aim, _evasion, _currentWeapon, _itemInventory);


    private void Start()
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

        _itemInventory = new List<ConsumableItem>();
        name = NamePicker.PickName();
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
