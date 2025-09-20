using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatroolPoint : MonoBehaviour
{
    public float PointRadius = 0.3f;

#if DEBUG
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, PointRadius);
    }
#endif
}
