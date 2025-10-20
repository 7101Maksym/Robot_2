using UnityEngine;

[CreateAssetMenu(fileName = "Inventory Item", menuName = "Inventory/Item")]
public class InventoryItem : ScriptableObject
{
    public string Name;
    public Sprite Icon;
    public string Description;
}
