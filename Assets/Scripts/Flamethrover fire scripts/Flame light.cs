using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Flamelight : MonoBehaviour
{
	[SerializeField] private Light2D _light;
	[SerializeField] private SpriteRenderer _sprite;

	private void Start()
	{
		_light.enabled = false;
	}

	private void LateUpdate()
	{
		if (_sprite.sprite != null && _sprite.sprite.GetPhysicsShapeCount() != 0)
		{
			List<Vector2> shape = new List<Vector2>();
			List<Vector3> preparedShape = new List<Vector3>();

			_sprite.sprite.GetPhysicsShape(0, shape);

			foreach (var point in shape)
			{
				preparedShape.Add(point);
			}

			_light.SetShapePath(preparedShape.ToArray());
			
			_light.enabled = true;
		}
		else
		{
			_light.enabled = false;
		}
	}
}
