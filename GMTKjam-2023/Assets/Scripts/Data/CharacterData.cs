using UnityEngine;
public class CharacterData 
{
    public int health;
    public int shield;

    public int luck;
    public int aim;
    public int evasion;

    public Weapon currentWeapon;

    public CharacterData(int health, int shield, int luck, int aim, int evasion, Weapon currentWeapon)
    {
        this.health = health;
        this.shield = shield;
        this.luck = luck;
        this.aim = aim;
        this.evasion = evasion;
        this.currentWeapon = currentWeapon;
    }
}
