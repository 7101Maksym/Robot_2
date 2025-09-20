using UnityEngine;

public class DemolitisionistStats : CurrentStats
{
    [SerializeField] private DefaultDemolitisionistStats _myStats;

    public float Attack { get; set; }

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        MaxHealths = _myStats.MaxHealths;
        MyHealths = MaxHealths;
        Attack = _myStats.Attack;
        Speed = _myStats.Speed;
    }
}
