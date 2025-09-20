using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class FlameHitboxSetter : MonoBehaviour
{
	[SerializeField] private SpriteRenderer _sprite;

	private PolygonCollider2D _collider;

	private void Awake()
	{
		_collider = GetComponent<PolygonCollider2D>();
		_collider.enabled = false;
	}

	private void LateUpdate()
	{
		if (_sprite.sprite != null)
		{
			int shapeCount = _sprite.sprite.GetPhysicsShapeCount();

			_collider.enabled = true;
			
			_collider.pathCount = shapeCount;

			for (int i = 0; i < shapeCount; i++)
			{
				List<Vector2> path = new List<Vector2>();

				_sprite.sprite.GetPhysicsShape(i, path);

				_collider.SetPath(i, path.ToArray());
			}
		}
		else
		{
			_collider.enabled = false;
		}
	}
}
