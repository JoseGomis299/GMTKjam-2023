using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Chest : MonoBehaviour
{
  [SerializeField] private List<ChestItem> itemList;
  [SerializeField] private List<ChestWeapon> weaponList;

  private void Start()
  {
    weaponList.Sort((x, y) => y.probability - x.probability);
    itemList.Sort((x, y) => y.probability - x.probability);
  }

  public Weapon GetWeapon()
  {
    float num = Random.value * 100;
    int prob = weaponList[0].probability;
    
    for (int i = 1; i < weaponList.Count; i++)
    {
      if (num <= prob + weaponList[i].probability && num >= prob)
        return weaponList[i].weapon;

      if (num < prob + weaponList[i].probability)
        return weaponList[i - 1].weapon;

      prob += weaponList[i].probability;
    }

    return null;
  }  
  
  public ConsumableItem GetConsumableItem()
  {
    float num = Random.value * 100;
    int prob = itemList[0].probability;
    
    for (int i = 1; i < itemList.Count; i++)
    {
      if (num <= prob + itemList[i].probability && num >= prob)
        return itemList[i].item;

      if (num < prob + itemList[i].probability)
        return itemList[i - 1].item;

      prob += itemList[i].probability;
    }

    return null;
  }
}
