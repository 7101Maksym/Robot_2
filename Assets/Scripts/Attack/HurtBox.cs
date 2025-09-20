using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtBox : MonoBehaviour
{
    [SerializeField] private CurrentStats _myStats;

    public float TakeDamage(float damage)
    {
        _myStats.MyHealths -= damage;

        return _myStats.MyHealths;
    }

#if DEBUG
    public GameObject root;
#endif
}
