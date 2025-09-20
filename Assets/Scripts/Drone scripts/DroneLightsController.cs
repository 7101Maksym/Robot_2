using UnityEngine;

public class DroneLightsController : MonoBehaviour
{
	private StateManager _stateManager;

	private void Awake()
	{
		_stateManager = GameObject.Find("StateManager").GetComponent<StateManager>();
	}

	public void SetRotateOfLight(int angle)
	{
		if (_stateManager.MovingState == MovingStates.Move)
		{
			if (angle < 180)
			{
				gameObject.transform.rotation = Quaternion.Euler(0, 0, -angle);
			}
			else
			{
				gameObject.transform.rotation = Quaternion.Euler(0, 0, 360 - angle);
			}
		}
	}
}
