using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemolitisionistMover : MonoBehaviour
{
    private Rigidbody2D _rb;

	private Vector2 _direction;
	private float _speed = 1f;

    private void Awake()
	{
		_rb = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		if (_rb.bodyType != RigidbodyType2D.Static)
		{
			_rb.velocity = _direction * _speed;
		}
	}

	public Vector2 SetNewDirection()
	{
        _direction = new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;

        return _direction;
	}

	public Vector2 SetDirectionToTarget(Transform target)
	{
		_direction = (target.position - transform.position).normalized;

		return _direction;
	}

    public Vector2 SetDirectionToTarget(Vector2 target)
    {
        _direction = ((Vector3)target - transform.position).normalized;

        return _direction;
    }

    public void SetZeroVelocity()
	{
		_direction = Vector2.zero;

		_rb.bodyType = RigidbodyType2D.Static;
	}

    public void SetSpeed(float speed)
    {
		_speed = speed;
    }
}
