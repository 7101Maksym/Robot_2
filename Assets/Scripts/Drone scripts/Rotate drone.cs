using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotatedrone : MonoBehaviour
{
	private readonly int[] angles = { 0, 22, 45, 67, 90, 112, 135, 157, 180, 202, 225, 247, 270, 292, 315, 337, 360 };

	private int angle = 0;

	public int GetAngle()
	{
		float angle = GetNowAngle();

		if (angle < 0)
		{
			angle = 180 + angle;
		}
		else if (angle > 0)
		{
			angle = -180 + angle;
		}
		else
		{
			angle = 180;
		}

		angle += 180f;

		for (int i = 0; i < angles.Length - 1; i++)
		{
			if (angle > angles[i])
			{
				this.angle = GetRotateAngle(angle, angles[i], angles[i + 1]);

				if (this.angle == 360)
				{
					this.angle = 0;
				}
			}
		}

		return this.angle;
	}

	private int GetRotateAngle(float angle, int min, int max)
	{
		if (angle >= min || angle <= max)
		{
			if (angle - min < max - angle)
			{
				return min;
			}
			else
			{
				return max;
			}
		}
		else if (angle < min)
		{
			return min;
		}
		else if (angle > max)
		{
			return max;
		}
		else
		{
			return 0;
		}
	}

	private int GetNowAngle()
	{
		Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 dronePosition = transform.position;
		Vector2 vectorToMouse = mousePosition - dronePosition;

		float controlAngle;
		int angleToMouse = (int)Vector2.Angle(transform.up, vectorToMouse);

		controlAngle = Vector2.Angle(transform.right.normalized, new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y).normalized);

		if (controlAngle < 90)
		{
			return angleToMouse;
		}
		else if (controlAngle > 90)
		{
			return -angleToMouse;
		}
		else
		{
			if (mousePosition.y < dronePosition.y)
			{
				return -180;
			}
			else if (mousePosition.y > dronePosition.y)
			{
				return 0;
			}
		}

		return 0;
	}
}
