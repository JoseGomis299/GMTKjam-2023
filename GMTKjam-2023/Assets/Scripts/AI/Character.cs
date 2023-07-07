using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterData characterData { get; private set; }

    private void Start()
    {
        characterData.InitializeStats();
    }

    public void ReceiveDamage(int damage)
    {
        characterData.shield -= damage;
        if (characterData.shield >= 0) return;
        
        characterData.health += characterData.shield;
        characterData.shield = 0;
    }

    public void GrabWeapon(Weapon weapon)
    {
        characterData.currentWeapon = weapon;
    }
}
