using System;
using UnityEngine;

[Serializable]
public class ChestWeapon 
{
    [field:SerializeReference] public Weapon weapon { get; private set; }
    [field:SerializeReference] public int probability { get; private set; }
}
