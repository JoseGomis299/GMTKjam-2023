using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDataUI : MonoBehaviour
{
    [SerializeField] private TMP_Text playerName;
    [SerializeField] private Image weaponImage;
    [SerializeField] private TMP_Text weaponTier;
    [SerializeField] private TMP_Text aim;
    [SerializeField] private TMP_Text luck;
    [SerializeField] private TMP_Text evasion;
    [SerializeField] private TMP_Text betAmount;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider shieldSlider;

    public void Initialize(CharacterData data, int betAmount)
    {
        playerName.text = $"PLAYER NAME:\n {data.name}";
        weaponImage.sprite = data.currentWeapon.sprite;

        string weaponTierText = "WEAPON TIER:\n";
        switch (data.currentWeapon.tier)
        {
            case ItemTier.Common: weaponTierText += "<color=#6E6E6E>COMMON</color>";
                break;
            case ItemTier.Uncommon: weaponTierText += "<color=#65935D>UNCOMMON</color>";
                break;
            case ItemTier.Rare: weaponTierText += "<color=#558DC3>RARE</color>";
                break;
            case ItemTier.Epic: weaponTierText += "<color=#815ABB>EPIC</color>";
                break;
            case ItemTier.Legendary: weaponTierText += "<color=#FFA61E>LEGENDARY</color>";
                break;
        }
        weaponTier.text = weaponTierText;
        
        aim.text = $"- AIM: {data.aim}";
        luck.text = $"- LUCK: {data.luck}";
        evasion.text = $"- EVASION: {data.evasion}";

        this.betAmount.text = $"BET AMOUNT:\n ${betAmount}";

        healthSlider.value = data.health;
        shieldSlider.value = data.shield;
    }
}
