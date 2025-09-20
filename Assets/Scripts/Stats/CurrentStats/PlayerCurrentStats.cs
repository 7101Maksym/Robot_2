using UnityEngine;

public class PlayerStats : CurrentStats
{
    [SerializeField] private DefaultPlayerStats _myStats;

    public float FlamethroverCooldown { get; set; }
    public float AttackFlamethrover { get; set; }
    public float AttackGun { get; set; }
    public float RunSpeed { get; set; }

    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        MaxHealths = _myStats.MaxHealths;
        MyHealths = MaxHealths;
        AttackFlamethrover = _myStats.FlamethroverAttack;
        AttackGun = _myStats.GunAttack;
        Speed = _myStats.NormalSpeed;
        RunSpeed = _myStats.RunSpeed;
        FlamethroverCooldown = _myStats.FlamethroverCooldown;
    }
}
