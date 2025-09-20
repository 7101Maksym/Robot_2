using UnityEngine;

[CreateAssetMenu(menuName = "Stats/PlayerStats")]
public class DefaultPlayerStats : ScriptableObject
{
    [Header("Carcass")]
    public float MaxHealths = 100f;
    [Header("Weapons")]
    public float FlamethroverAttack = 5f;
    public float FlamethroverCooldown = 0.5f;
    public float GunAttack = 2f;
    [Header("Movement")]
    public float NormalSpeed = 5f;
    public float RunSpeed = 8f;
}
