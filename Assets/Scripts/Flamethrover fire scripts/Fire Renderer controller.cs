using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class FireRendererController : MonoBehaviour
{
    private Animator _animator;

    public UnityEvent OnFlamethroverStopShooting;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Shoot(GunStates state)
    {
        _animator.Play("", 0, 0f);

        if (state == GunStates.Flamethrover_vertical)
        {
            if (_animator.GetFloat("HorizontalOrVertical") == 0f)
            {
                _animator.SetFloat("HorizontalOrVertical", 1f);
                StartCoroutine(StopShoot());
            }
        }
        else if (state == GunStates.Flamethrover_horizontal)
        {
            if (_animator.GetFloat("HorizontalOrVertical") == 0f)
            {
                _animator.SetFloat("HorizontalOrVertical", -1f);
                StartCoroutine(StopShoot());
            }
        }
        else
        {
            _animator.SetFloat("HorizontalOrVertical", 0f);
        }
    }

    private IEnumerator StopShoot()
    {
        yield return new WaitForSeconds(2.9f);

        _animator.SetFloat("HorizontalOrVertical", 0f);

        OnFlamethroverStopShooting?.Invoke();
    }

    public void SetAngle(float angle)
    { 
        _animator.SetFloat("Angle", angle);
    }
}
