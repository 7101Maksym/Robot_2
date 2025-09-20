using System.Collections;
using UnityEngine;

public class HitBoxManager : MonoBehaviour
{
    private enum AttackType
    {
        Impulse,
        Continuous
    }

    private enum IsHitting
    {
        Yes,
        No
    }

    [SerializeField] private HitBox _hitBox;
    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private AttackType _attackType;

    private IsHitting _isHitting = IsHitting.No;

    private float cooldownTime = 0f;
    private bool _isHiting = false;

    public void SetParam(float damage, float cooldown)
    {
        _hitBox.SetParametrs(_targetMask, damage);

        cooldownTime = cooldown;
    }

    public void SetParam(float damage)
    {
        if (_attackType != AttackType.Impulse)
        {
            Debug.LogError("Continious attack type need cooldown");
            return;
        }

        _hitBox.SetParametrs(_targetMask, damage);
    }

    private void FixedUpdate()
    {
        if (_isHitting == IsHitting.Yes)
        {
            if (_attackType == AttackType.Impulse && _isHiting == false)
            {
                _hitBox.Hit();

                _isHiting = true;
            }
            else if (_attackType == AttackType.Continuous && _isHiting == false)
            {
                StartCoroutine(ContiniousHit());

                _isHiting = true;
            }
        }
    }

    public void StartHit()
    {
        _isHitting = IsHitting.Yes;
    }

    public void StopHit()
    {
        _isHitting = IsHitting.No;
    }

    private IEnumerator ContiniousHit()
    {
        yield return new WaitForSeconds(cooldownTime);

        _hitBox.Hit();

        _isHiting = false;
    }
}