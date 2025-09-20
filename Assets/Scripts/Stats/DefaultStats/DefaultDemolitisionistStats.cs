using UnityEngine;

[CreateAssetMenu(menuName = "Stats/DemolitisionistStats")]
public class DefaultDemolitisionistStats : ScriptableObject
{
    [Header("Carcass")]
    public float MaxHealths = 100f;
    [Header("Weapons")]
    public float Attack = 50f;
    [Header("Movement")]
    public float Speed = 1f;
}
