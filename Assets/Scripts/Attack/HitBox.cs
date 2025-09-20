using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    private LayerMask _targetMask;
    private float _damage;

    public void Hit()
    {
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(_targetMask);

        List<Collider2D> colliders = new List<Collider2D>();
        Physics2D.OverlapCollider(GetComponent<Collider2D>(), contactFilter, colliders);

        foreach (var collider in colliders)
        {
            float healths;
            HurtBox _hurtBox = collider.gameObject.GetComponent<HurtBox>();

            healths = _hurtBox.TakeDamage(_damage);
            Debug.Log($"{gameObject.transform.root} hit {_hurtBox.root}, {_hurtBox.root} healths = {healths}");
        }
    }

    public void SetParametrs(LayerMask n, float damage)
    {
        _targetMask = n;
        _damage = damage;
    }
}
