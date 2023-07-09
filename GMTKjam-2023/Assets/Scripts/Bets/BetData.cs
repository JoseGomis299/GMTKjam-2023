using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetData
{
   public string name;
   public float multiplier;

   public BetData(CharacterData characterData)
   {
      name = characterData.name;
      multiplier = CalculateMultiplier(characterData);
   }

   private float CalculateMultiplier(CharacterData data)
   {
      float res = 1.5f;
      
      if(data.luck != 0) res -= 0.01f / (data.luck / 100f);
      if(data.aim != 0 )res -= 0.04f / (data.aim / 100f);
      if(data.evasion != 0) res -= 0.03f / (data.evasion / 100f);

      if (MapManager.instance.RoundNumber > 0)
      {
         res += 0.03f / (data.health / 100f);
         if(data.shield > 0) res += 0.03f / (data.shield / 100f);

         res += 0.05f / ((int)data.currentWeapon.tier / 5f);
      }

      if (res < 1) res = 1;
      res = (int)(res * 100) / 100f;

      return res;
   }
}
