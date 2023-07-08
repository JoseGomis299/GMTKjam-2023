using UnityEngine;

[CreateAssetMenu(fileName = "ConsumableItem")]
public class ConsumableItem : ScriptableObject
{
    [SerializeField] private Sprite sprite;

    [field:SerializeReference] public ItemTier tier { get; private set; }
    
    [field:SerializeReference] public int healthRegeneration { get; private set; }
    [field:SerializeReference] public int shieldRegeneration { get; private set; }
}
