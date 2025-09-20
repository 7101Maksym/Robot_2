using UnityEngine;

public class NoteRenderer : MonoBehaviour
{
    [SerializeField] private float _length;

    public GameObject _object;

    private LineRenderer _line;

    private Vector3 _dir, _endPos;

    private void Awake()
    {
        _line = GetComponentInChildren<LineRenderer>();
    }

    private void Update()
    { 
        if (_object != null)
        {
            _dir = (_object.transform.position - transform.position).normalized * _length;
            _endPos = transform.position + _dir;

            _line.SetPosition(2, _endPos);
            _line.SetPosition(1, new Vector3(_endPos.x - 1, _endPos.y));
            _line.SetPosition(0, _object.transform.position);
        }
    }
}
