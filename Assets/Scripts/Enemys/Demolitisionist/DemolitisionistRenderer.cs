using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemolitisionistRenderer : MonoBehaviour
{
    [SerializeField] private DemolitisionistAI _AI;
    
    private Animator _animator;

    private int _horizontal, _vertical;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_AI.Status != DemolitisionistAI.MyStatus.Destroyed)
        {
            _animator.SetFloat("Horizontal", _horizontal);
            _animator.SetFloat("Vertical", _vertical);
        }
        else
        {
            _animator.SetBool("IsDestroyed", true);

            StartCoroutine(ExplosionDelay());
        }
    }

    public void SetAnimatingDirection(Vector2 direction)
    {
        float X = direction.x, Y = direction.y;

        if (Mathf.Abs(X) > Mathf.Abs(Y))
        {
            _vertical = 0;

            if (X > 0)
            {
                _horizontal = 1;
            }
            else
            {
                _horizontal = -1;
            }
        }
        else
        {
            _horizontal = 0;

            if (Y > 0)
            {
                _vertical = 1;
            }
            else
            {
                _vertical = -1;
            }
        }
    }

    private IEnumerator ExplosionDelay()
    {
        yield return new WaitForSeconds(1);

        _animator.SetBool("Explosion", true);

        StartCoroutine(DeleteDelay());
    }

    private IEnumerator DeleteDelay()
    {
        yield return new WaitForSeconds(0.43f);

        _AI.Damage();
    }
}
