using System.Collections.Generic;
using UnityEngine;
public class CharacterData
{
    public string name;
    
    public int health;
    public int shield;

    public int luck;
    public int aim;
    public int evasion;

    public Weapon currentWeapon;
    public List<ConsumableItem> itemInventory;


    public CharacterData(string name, int health, int shield, int luck, int aim, int evasion, Weapon currentWeapon, List<ConsumableItem> itemInventory)
    {
        this.name = name;
        this.health = health;
        this.shield = shield;
        this.luck = luck;
        this.aim = aim;
        this.evasion = evasion;
        this.currentWeapon = currentWeapon;
        this.itemInventory = itemInventory;
    }
}
