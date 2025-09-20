using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour
{
    [SerializeField] private HitBoxManager _flamethroverHitBox;

    private StateManager _stateManager;
    private DroneMove _droneMove;
    private FireRendererController _fireRendererController;
    private DroneRendererController _droneRendererController;
    private InteractionScript _interactionScript;  
    private Rotatedrone _rotateDrone;
    private DroneLightsController _droneLightsController;
    private PlayerStats _playerState;

    private bool _isInShooting = false;

    private void Awake()
    {
        _stateManager = GameObject.Find("StateManager").GetComponent<StateManager>();

        _droneMove = GetComponent<DroneMove>();
        _fireRendererController = GetComponentInChildren<FireRendererController>();
        _droneRendererController = GetComponentInChildren<DroneRendererController>();
        _interactionScript = GetComponent<InteractionScript>();
        _rotateDrone = GetComponent<Rotatedrone>();
        _droneLightsController = GetComponentInChildren<DroneLightsController>();
        _playerState = GetComponent<PlayerStats>();
    }

    private void Start()
    {
        _droneMove.SetSpeed(_playerState.Speed, _playerState.RunSpeed);

        if (_flamethroverHitBox != null)
        {
            _flamethroverHitBox.SetParam(_playerState.AttackFlamethrover, _playerState.FlamethroverCooldown);
        }
        else
        {
            Debug.LogError("No Flamethrover HitBox on Drone");
        }
    }

    public void SetInput(InputHandler.InputData data)
    {
        SwitchState(data);

        if (_playerState.MyHealths > 0)
        {
            switch (_stateManager.MovingState)
            {
                case MovingStates.Move:
                    _droneMove.SetDirection((int)data.Movement.x, (int)data.Movement.y);

                    if (data.Run)
                    {
                        _droneMove.Run();
                    }
                    else
                    {
                        _droneMove.Normal();
                    }

                    int myAngle = _rotateDrone.GetAngle();
                    _droneRendererController.Rotate(myAngle);
                    _droneRendererController.Move(new Vector2(data.Movement.y, data.Movement.x));
                    _fireRendererController.SetAngle(myAngle);
                    _droneLightsController.SetRotateOfLight(myAngle);

                    break;
                case MovingStates.NotMove:
                    _droneMove.Stop();
                    break;
                default:
                    break;
            }

            switch (_stateManager.ShootingState)
            {
                case ShootingStates.Shoot:
                    if (!_isInShooting)
                    {
                        _isInShooting = true;

                        _fireRendererController.Shoot(_stateManager.GunState);
                        _droneRendererController.Shoot(_stateManager.GunState);

                        if (_stateManager.GunState == GunStates.Flamethrover_vertical || _stateManager.GunState == GunStates.Flamethrover_horizontal)
                        {
                            _flamethroverHitBox.StartHit();
                        }
                    }
                    break;
                default:
                    _flamethroverHitBox.StopHit();

                    break;
            }

            switch (_stateManager.FlyingState)
            {
                case FlyingStates.Takeoff:
                    _droneRendererController.Takeoff();

                    break;
                case FlyingStates.Landing:
                    _droneRendererController.Landing();

                    break;
                default:
                    break;
            }
        }
        else
        {
            _droneMove.Stop();
            _droneRendererController.Death();
        }
    }

    private void SwitchState(InputHandler.InputData data)
    {
        if (data.Shoot && _stateManager.ShootingState != ShootingStates.Shoot && _stateManager.FlyingState == FlyingStates.InAir)
        {
            _stateManager.ShootingState = ShootingStates.Shoot;
        }
        
        if (_stateManager.ShootingState == ShootingStates.Shoot || _stateManager.FlyingState != FlyingStates.InAir)
        {
            _stateManager.MovingState = MovingStates.NotMove;
        }
        else
        {
            _stateManager.MovingState = MovingStates.Move;
        }

        if (data.TakeoffOrLand > 0 && _stateManager.FlyingState == FlyingStates.OnGround)
        {
            _stateManager.FlyingState = FlyingStates.Takeoff;
        }
        else if (data.TakeoffOrLand < 0 && _stateManager.FlyingState == FlyingStates.InAir)
        {
            _stateManager.FlyingState = FlyingStates.Landing;
        }
    }

    public void SetMoveState()
    {
        _stateManager.MovingState = MovingStates.Move;
    }

    public void SetNotShootingState()
    {
        _stateManager.ShootingState = ShootingStates.NotShoot;
        _isInShooting = false;
    }

    public void SetStateInAir()
    {
        _stateManager.FlyingState = FlyingStates.InAir;
        _stateManager.MovingState = MovingStates.Move;
    }

    public void SetStateOnGround()
    {
        _stateManager.FlyingState = FlyingStates.OnGround;
    }
}
