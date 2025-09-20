using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    [SerializeField] private GunStates DefaultGunState;
    [SerializeField] private MovingStates DefaultMovingState;
    [SerializeField] private ShootingStates DefaultShootingState;
    [SerializeField] private FlyingStates DefaultFlyingState;

    public GunStates GunState { get; set; }

    public MovingStates MovingState { get; set; }

    public ShootingStates ShootingState { get; set; }

    public FlyingStates FlyingState { get; set; }

    private void Awake()
    {
        GunState = DefaultGunState;
        MovingState = DefaultMovingState;
        ShootingState = DefaultShootingState;
        FlyingState = DefaultFlyingState;
    }
}
