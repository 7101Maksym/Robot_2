using UnityEngine;

public class DemolitisionistDestroyDetect : MonoBehaviour
{
	[SerializeField] private LayerMask _targetMask;

	private DemolitisionistAI _AI;

	private void Awake()
	{
		_AI = GetComponentInParent<DemolitisionistAI>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (((1 << collision.gameObject.layer) & _targetMask) != 0)
        {
			_AI.Status = DemolitisionistAI.MyStatus.Destroyed;
		}
	}
}
