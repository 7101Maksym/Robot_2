using UnityEditor;
using UnityEngine;

public class PatroolPathsManager : MonoBehaviour
{
	public int StartPoint = 0;
	public Transform[] PatroolPoints;

	[Header("Interface settings")]
	[SerializeField] private Color _patroolColor = Color.gray;
	[SerializeField] private float _pointRadius = 0.5f;

	[Header("Arrow settings")]
	[SerializeField] private float _arrowLength = 0.5f;
	[SerializeField] private float _arrowAngle = 0.2f;

	private PointManager _pointManager;

	private void Start()
	{
		PatroolPoints = _pointManager.GetPoints();
	}

	private void OnValidate()
	{
		if (_pointManager == null)
        {
            _pointManager = GetComponent<PointManager>();
        }

        PatroolPoints = _pointManager.GetPoints();
	}

#if DEBUG
	private void OnDrawGizmos()
	{
		Gizmos.color = _patroolColor;

		for (int i = 0; i < PatroolPoints.Length; i++)
		{
			if (i != StartPoint - 1)
			{
				Gizmos.color = _patroolColor;
			}
			else
			{
				Gizmos.color = Color.red;
			}

			Handles.color = Color.white;
			Handles.Label(PatroolPoints[i].transform.position + new Vector3(_pointRadius + 0.5f, 0, 0), $"Point {i + 1}");

			Gizmos.DrawLine(PatroolPoints[i].transform.position, PatroolPoints[(i + 1) % PatroolPoints.Length].transform.position);

			DrawArrows(i);
		}
	}

	private void DrawArrows(int i)
	{
		Vector2 nowVector = (PatroolPoints[(i + 1) % PatroolPoints.Length].transform.position - PatroolPoints[i].transform.position).normalized * _arrowLength;

		Vector2 startPoint = (PatroolPoints[(i + 1) % PatroolPoints.Length].transform.position - PatroolPoints[i].transform.position).normalized;
		startPoint *= Vector2.Distance(PatroolPoints[i].transform.position, PatroolPoints[(i + 1) % PatroolPoints.Length].transform.position) / 2;
		startPoint += (Vector2)PatroolPoints[i].transform.position;

		Vector2 p1, p2;
		p1 = startPoint - nowVector + (Vector2.Perpendicular(nowVector).normalized * _arrowAngle);
		p2 = startPoint - nowVector - (Vector2.Perpendicular(nowVector).normalized * _arrowAngle);

		Gizmos.DrawLine(startPoint, p1);
		Gizmos.DrawLine(startPoint, p2);
	}

#endif
}
