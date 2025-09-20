using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
	private DroneController _droneController;

    public struct InputData
    {
        public Vector2 Movement;
        public float TakeoffOrLand;
        public bool Shoot;
        public bool Run;
        public bool Interaction;
    }

	private InputData _data;

    private void Awake()
	{
        _droneController = GetComponent<DroneController>();
    }

    private void Update()
    {
        _droneController.SetInput(_data);
    }


    public void OnMove(InputAction.CallbackContext context)
	{
		var movement = context.ReadValue<Vector2>();
		_data.Movement = new Vector2((int)movement.y, (int)movement.x);
	}

	public void OnTakeoffOrLand(InputAction.CallbackContext context)
	{
		var takeoffOrLand = context.ReadValue<float>();

		_data.TakeoffOrLand = takeoffOrLand;
    }

	public void OnShoot(InputAction.CallbackContext context)
	{
		var shoot = context.performed;

        _data.Shoot = shoot;
    }

	public void OnRun(InputAction.CallbackContext context)
	{
		_data.Run = context.performed;
    }

	public void OnInteraction(InputAction.CallbackContext context)
	{
		_data.Interaction = context.started;
    }
}
