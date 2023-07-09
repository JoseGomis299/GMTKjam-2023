using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon")]
public class Weapon : ScriptableObject
{
   [SerializeField] public Sprite sprite;
   
   [field:SerializeReference] public int damage { get; private set; }
   [field:SerializeReference] public ItemTier tier { get; private set; }
   
   //Si tenemos tiempo...
   
   // [field:SerializeReference] public float cadency { get; private set; }
   // [field:SerializeReference] public float reloadTime { get; private set; }
   //
   // [field:SerializeReference] public int maxAmmo { get; private set; }
   // public int currentAmmo;
   //
   // private void OnEnable()
   // {
   //    currentAmmo = maxAmmo;
   // }
}
