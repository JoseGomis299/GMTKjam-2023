using UnityEngine;

public class Character : MonoBehaviour
{
    private int _health;
    private int _shield;

    private int _luck;
    private int _aim;
    private int _evasion;

    private Weapon _currentWeapon;
    
    public CharacterData GetCharacterData() => new CharacterData(_health, _shield, _luck, _aim, _evasion, _currentWeapon);

    private void Start()
    {
        InitializeStats();
    }

    public void ReceiveTrueDamage(int damage)
    {
        _health -= damage;
        if(_health < 0) Die();
    }
    
    public void ReceiveDamage(int damage)
    {
        _shield -= damage;
        if (_shield >= 0) return;
        
        _health += _shield;
        _shield = 0;
        
        if(_health < 0) Die();
    }

    public void GrabWeapon(Weapon weapon)
    {
        _currentWeapon = weapon;
    }
    
    private void Die()
    {
        
    }
    
    private void InitializeStats()
    {
        _health = 100;
        _shield = 0;

        _luck = Random.Range(-100, 101);
        _aim = Random.Range(-100, 101);
        _evasion = Random.Range(-100, 101);
    }
}
