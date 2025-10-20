using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : MonoBehaviour
{
    [SerializeField] private float _pointRadius = 0.3f;

    private Point[] _points;

    private void Awake()
    {
        _points = GetComponentsInChildren<Point>();
    }

    private void OnValidate()
    {
        _points = GetComponentsInChildren<Point>();

        foreach (var point in _points)
        {
            point.PointRadius = _pointRadius;
        }
    }

    public Transform[] GetPoints()
    {
        Transform[] pointsTransforms = new Transform[_points.Length];

        for (int i = 0; i < _points.Length; i++)
        {
            pointsTransforms[i] = _points[i].transform;
        }

        return pointsTransforms;
    }
}
