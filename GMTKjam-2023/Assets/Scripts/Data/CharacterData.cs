using UnityEngine;
public class CharacterData 
{
    public int health;
    public int shield;

    public int luck;
    public int aim;
    public int evasion;

    public Weapon currentWeapon;
    
    public void InitializeStats()
    {
        health = 100;
        shield = 0;

        luck = Random.Range(-100, 101);
        aim = Random.Range(-100, 101);
        evasion = Random.Range(-100, 101);
    }
}
