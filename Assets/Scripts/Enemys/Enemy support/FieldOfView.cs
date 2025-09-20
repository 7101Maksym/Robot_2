using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FieldOfView : MonoBehaviour
{ 
	[SerializeField] private float _viewRadius = 5f;

	[Range(0, 360)]
	[SerializeField] private float _viewAngle = 90f;
	[SerializeField] private LayerMask _obstaclesLayer;
    [SerializeField] private Light2D _field;

	public Transform IsVisible(LayerMask targetMask)
	{
        Collider2D[] targetColliders = Physics2D.OverlapCircleAll(transform.position, _viewRadius, targetMask);
		List<Transform> targets = new List<Transform>(), targetsInViev = new List<Transform>();

        foreach (Collider2D collider in targetColliders)
		{
			targets.Add(collider.transform);
        }

		foreach (Transform target in targets)
		{
			if (Vector2.Distance(transform.position, target.position) <= _viewRadius)
			{
				Vector2 direction = (target.position - transform.position).normalized;

				if (!Physics2D.Raycast(transform.position, direction, Vector2.Distance(target.position, transform.position), _obstaclesLayer))
				{
					if (Vector2.Angle(transform.up, target.position - transform.position) <= _viewAngle / 2)
					{
						targetsInViev.Add(target);
                    }
				}
			}
		}

		if (targetsInViev.Count != 0)
		{
			float minDistance = Vector2.Distance(targetsInViev[0].position, transform.position);
			Transform myTarget = targetsInViev[0];

            foreach (Transform target in targetsInViev)
			{
				if (Vector2.Distance(target.position, transform.position) < minDistance)
                {
                    minDistance = Vector2.Distance(target.position, transform.position);
                    myTarget = target;
                }
            }

			return myTarget;
		}

		return null;
    }

#if DEBUG
	private void OnDrawGizmos()
	{
		_field.pointLightOuterRadius = _viewRadius;
		_field.pointLightInnerRadius = _viewRadius * 0.9f;
		_field.pointLightInnerAngle = _viewAngle;
		_field.pointLightOuterAngle = _viewAngle;

		float radA = (transform.rotation.eulerAngles.z - _viewAngle / 2 + 90) * Mathf.Deg2Rad;
		float radB = (transform.rotation.eulerAngles.z + _viewAngle / 2 + 90) * Mathf.Deg2Rad;
		Vector2 d1 = new Vector2(Mathf.Cos(radA), Mathf.Sin(radA));
		Vector2 d2 = new Vector2(Mathf.Cos(radB), Mathf.Sin(radB));

		Gizmos.color = Color.yellow;
		Handles.color = Color.yellow;
		Handles.DrawWireArc(transform.position, transform.forward, d1, _viewAngle, _viewRadius);
		Gizmos.DrawLine(transform.position, transform.position + (Vector3)d1 * _viewRadius);
		Gizmos.DrawLine(transform.position, transform.position + (Vector3)d2 * _viewRadius);
	}
#endif
}
