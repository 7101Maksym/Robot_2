using UnityEngine;

public class DroneMove : MonoBehaviour
{
    private Rigidbody2D _rb;

    private float NormalSpeed = 5f, RunSpeed = 8f;
    private Vector2 forvard, back, left, right;
    private Vector2 _direction;
    private int WS, AD;
    private float _speed = 5f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        back = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        back = back.normalized;
        forvard = -back.normalized;
        left = Vector2.Perpendicular(forvard).normalized;
        right = -left;
    }

    private void FixedUpdate()
    {
        SetDirectionInProcess();
        _rb.velocity = _direction * _speed;
    }

    private void SetDirectionInProcess()
    {
        if (WS == 0 && AD == 0)
        {
            _direction = Vector2.zero;
            return;
        }
        else if (WS == 0 && AD == 1)
        {
            _direction = right;
            return;
        }
        else if (WS == 0 && AD == -1)
        {
            _direction = left;
            return;
        }
        else if (WS == 1 && AD == 0)
        {
            _direction = forvard;
            return;
        }
        else if (WS == -1 && AD == 0)
        {
            _direction = back;
            return;
        }
    }

    public void SetDirection(int WS, int AD)
    {
        this.WS = WS;
        this.AD = AD;
    }

    public void Run()
    {
        _speed = RunSpeed;
    }

    public void Normal()
    {
        _speed = NormalSpeed;
    }

    public void Stop()
    {
        _speed = 0f;
    }

    public void SetSpeed(float normalSpeed, float runSpeed)
    {
        NormalSpeed = normalSpeed;
        RunSpeed = runSpeed;
        _speed = NormalSpeed;
    }
}
