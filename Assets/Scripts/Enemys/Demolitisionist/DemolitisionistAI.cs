using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemolitisionistAI : MonoBehaviour
{
	public enum MyStatus
	{
		Moving,
		Destroyed,
		PlayerDetected
	}

	private enum MovingType
	{
		Random,
		Patrool
	}

	[SerializeField] private DemolitisionistRenderer _renderer;
	[SerializeField] private DemolitisionistMover _mover;
	[SerializeField] private PatroolPathsManager _paths;

	[Header("Random moving settings")]
	[Range(0.1f, 5f)]
	[SerializeField] private float _neededDistanceToObstacle = 1f;
	
	[Header("Type of moving")]
	[SerializeField] private MovingType _movingType;

	[Header("Obstacles detect settings")]
	[SerializeField] private LayerMask _obstacles;
	
	[Header("Object for attak")]
	[SerializeField] LayerMask _targetLayer;

	[Header("On patrol")]
	[SerializeField] private float _neededDistanceToPoint = 0.1f;
	public List<Vector2> _points;

	[Header("Status")]
	public MyStatus Status = MyStatus.Moving;

	private FieldOfView _view;
	private DemolitisionistStats _stats;
	private Transform _target;
	private HitBoxManager _hitBox;

	private bool _isMoving = false;
	private Vector2 _direction;
	private int _pointIndex = 0;

	private void Awake()
	{
		_stats = GetComponent<DemolitisionistStats>();
		_view = GetComponentInChildren<FieldOfView>();
		_hitBox = GetComponentInChildren<HitBoxManager>();

		_mover.SetSpeed(_stats.Speed);

		if (_movingType == MovingType.Patrool)
		{
			if (_paths != null)
			{
				foreach (PatroolPoint point in _paths.PatroolPoints)
				{
					_points.Add(point.transform.position);
				}

				_pointIndex = (_paths.StartPoint + _points.Count - 2) % _points.Count;

				_direction = _mover.SetDirectionToTarget(_points[_paths.StartPoint]);
				_renderer.SetAnimatingDirection(_direction);
			}
			else
			{
				Debug.LogError($"No Patrool Path assigned to {gameObject.name}");
			}
		}
		else
		{
			_direction = _mover.SetNewDirection();
			_renderer.SetAnimatingDirection(_direction);
		}
	}

	private void Start()
	{
		_hitBox.SetParam(_stats.Attack);
	}

	private void FixedUpdate()
	{
		if (_stats.MyHealths <= 0 && Status != MyStatus.Destroyed)
		{
			Status = MyStatus.Destroyed;
		}

		if (_movingType != MovingType.Patrool)
		{
			if (Physics2D.Raycast(transform.position, _direction, _neededDistanceToObstacle, _obstacles))
			{
				_direction = _mover.SetNewDirection();
				_renderer.SetAnimatingDirection(_direction);
			}
		}
		else
		{
			if (Vector2.Distance(transform.position, _points[_pointIndex]) <= _neededDistanceToPoint)
			{
				_isMoving = false;
			}
		}

		if (_view != null)
		{
			RotateFieldOfView();
			_target = _view.IsVisible(_targetLayer);
		}

		switch (Status)
		{
			case MyStatus.Moving:
				if (_target)
				{
					Status = MyStatus.PlayerDetected;
				}

				if (!_isMoving)
				{
					if (_movingType != MovingType.Patrool)
					{
						_isMoving = true;

						StartCoroutine(SettingNewDirection(true));
					}
					else
					{
						_isMoving = true;

						_pointIndex = (_pointIndex + 1) % _points.Count;

						_direction = _mover.SetDirectionToTarget(_points[_pointIndex]);
						_renderer.SetAnimatingDirection(_direction);
					}
				}

				break;
			case MyStatus.Destroyed:
				if (_view != null)
				{
					Destroy(_view.gameObject);
					_view = null;
				}

				_mover.SetZeroVelocity();

				break;
			default:
				if (!_target)
				{
					Status = MyStatus.Moving;
				}
				else
				{
					_isMoving = false;

					_mover.SetDirectionToTarget(_target);

					_direction = _target.position - transform.position;
					_renderer.SetAnimatingDirection(_direction);
				}

				break;
		}
	}

	public IEnumerator SettingNewDirection(bool use_delay)
	{
		if (use_delay)
		{
			yield return new WaitForSeconds(10);
		}
		else
		{
			yield return null;
		}

		do 
		{
			_direction = _mover.SetNewDirection();
		} while (Physics2D.Raycast(transform.position, _direction, _neededDistanceToObstacle, _obstacles));

		_renderer.SetAnimatingDirection(_direction);

		_isMoving = false;
	}

	private float GetRightAngle(Vector2 direct)
	{
		float angle = Vector2.Angle(direct, transform.up);
		float controlAnglel = Vector2.Angle(direct, transform.right);

		if (controlAnglel > 90)
		{
			return angle;
		}
		else
		{
			return -angle;
		}
	}

	private void RotateFieldOfView()
	{
		float angle = GetRightAngle(_direction);

		_view.transform.rotation = Quaternion.Euler(0, 0, angle);
	}

	public void Damage()
	{
		_hitBox.StartHit();
		Invoke(nameof(Destr), 0.1f);
    }

	public void Destr()
	{
		Destroy(gameObject);
    }

#if DEBUG
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.blue;

		if (_movingType == MovingType.Random)
		{
			Gizmos.DrawWireSphere(transform.position, _neededDistanceToObstacle);
		}
		else
		{
			Gizmos.DrawWireSphere(transform.position, _neededDistanceToPoint);
		}
	}
#endif
}
