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

  public Weapon GetWeapon(int luck)
  {
    float num = Random.value * 100 + luck;
    int prob = weaponList[0].probability;
    if (num >= 100) return weaponList[^1].weapon;
    
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
  
  public ConsumableItem GetConsumableItem(int luck)
  {
    float num = Random.value * 100 + luck;
    int prob = itemList[0].probability;
    if (num >= 100) return itemList[^1].item;

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

  public void GetObjects(int luck, out Weapon weapon, out ConsumableItem item)
  {
    weapon = GetWeapon(luck);
    item = GetConsumableItem(luck);
    Destroy(gameObject);
  }
  
}
