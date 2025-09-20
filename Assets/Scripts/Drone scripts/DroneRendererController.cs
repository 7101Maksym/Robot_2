using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DroneRendererController : MonoBehaviour
{
	private Animator _animator;

	public UnityEvent OnCanMove, OnLanded, OnInAir;

	private enum Actions
	{
		Moving = 1,
		Shooting = 2,
		Landing = 3,
		Healths = 4
	}

	private void Awake()
	{
		_animator = GetComponent<Animator>();

		SetTypeOfAction(Actions.Moving);
	}

	private void SetTypeOfAction(Actions type)
	{
		_animator.SetFloat("TypeOfAction(1-Moving)", (float)type);
	}

	public void Move(Vector2 direction)
	{
		if (direction.y != 0)
		{
			_animator.SetFloat("FlyForvardBack", direction.normalized.y);
			_animator.SetFloat("FlyLeftRight", 0f);
		}
		else if (direction.x != 0)
		{
			_animator.SetFloat("FlyLeftRight", direction.normalized.x);
			_animator.SetFloat("FlyForvardBack", 0f);
		}
		else
		{
			_animator.SetFloat("FlyLeftRight", 0f);
			_animator.SetFloat("FlyForvardBack", 0f);
		}
	}

	public void Rotate(float angle)
	{
		_animator.SetFloat("Angle", angle);
	}

	public void Death()
	{
        SetTypeOfAction(Actions.Healths);
        _animator.SetFloat("Healths(0-Death)", 0f);
    }

	public void Shoot(GunStates state)
	{
		SetTypeOfAction(Actions.Shooting);

        _animator.Play("", 0, 0f);

		_animator.SetFloat("TypeOfAction(1-Moving)", 2f);

		if (state == GunStates.Flamethrover_vertical)
		{
			_animator.SetFloat("GunType(1-Flamethrover)", 1f);
			_animator.SetFloat("ShootingType(1-Horizontal)", 0f);
		}
		else if (state == GunStates.Flamethrover_horizontal)
		{
			_animator.SetFloat("GunType(1-Flamethrover)", 1f);
			_animator.SetFloat("ShootingType(1-Horizontal)", 1f);
		}
		else if (state == GunStates.Gun_horizontal)
		{
			_animator.SetFloat("GunType(1-Flamethrover)", 0f);
			_animator.SetFloat("ShootingType(1-Horizontal)", 1f);
		}
		else if (state == GunStates.Gun_vertical)
		{
			_animator.SetFloat("GunType(1-Flamethrover)", 0f);
			_animator.SetFloat("ShootingType(1-Horizontal)", 0f);
		}

		StartCoroutine(SetMovingState());
    }

	public void Hit()
	{

	}

	public void Landing()
	{
        SetTypeOfAction(Actions.Landing);

        _animator.SetFloat("Landing(0-StayLanded)", -1f);

        StartCoroutine(Landed());
    }

	public void Takeoff()
	{
		_animator.SetFloat("Landing(0-StayLanded)", 1f);

        SetTypeOfAction(Actions.Landing);

        StartCoroutine(TakeoffCoroutine());
    }

	private IEnumerator SetMovingState()
	{
        yield return new WaitForSeconds(2f);
		
		SetTypeOfAction(Actions.Moving);

		yield return new WaitForSeconds(1f);

		OnCanMove?.Invoke();
    }

    private IEnumerator Landed()
	{
        yield return new WaitForSeconds(0.79f);

        _animator.Play("", 0, 0f);

        _animator.SetFloat("Landing(0-StayLanded)", 0f);

        OnLanded?.Invoke();
    }

    private IEnumerator TakeoffCoroutine()
	{
        yield return new WaitForSeconds(0.79f);

		SetTypeOfAction(Actions.Moving);

        OnInAir?.Invoke();
    }
}
