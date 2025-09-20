using UnityEngine;

public class Monolit_Script : MonoBehaviour
{
    [SerializeField] private int _neededMaterials = 10;
    
    private Main_Portal _portal;
    private Animator _animator;

    private int _nowMaterial = 0;
    private bool _active = false;

    private void Awake()
    {
        _portal = FindObjectOfType<Main_Portal>();
        _animator = GetComponentInChildren<Animator>();
    }

    public void Action()
    {
        _nowMaterial++;
    }

    private void Update()
    {
        if (_nowMaterial == _neededMaterials && !_active)
        {
            _active = true;

            if (_portal.IsActive1)
            {
                _portal.IsActive2 = true;
            }
            else
            {
                _portal.IsActive1 = true;
            }

            _animator.SetBool("IsActive", true);
        }
    }
}
