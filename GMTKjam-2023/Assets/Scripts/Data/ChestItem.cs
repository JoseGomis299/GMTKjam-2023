using System;
using UnityEngine;

[Serializable]
public class ChestItem 
{
    [field:SerializeReference] public ConsumableItem item { get; private set; }
    [field:SerializeReference] public int probability { get; private set; }
}
