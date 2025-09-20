using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private Transform _collector;
    [SerializeField] private GameObject _enemy;
    [SerializeField] private float _generatingDelay = 20f;
    [SerializeField] private Transform _target;
    [SerializeField] private int _enemyCount;

    private Transform[] _positions;

    private bool _canGenerate = true;

    private void Awake()
    {
        _positions = gameObject.transform.Find("GeneratePoints").GetComponentsInChildren<Transform>();
    }

    private void Start()
    {
        if (_collector.transform.childCount < _enemyCount)
        {
            StartCoroutine(AddEnemy());
        }
    }

    private void FixedUpdate()
    {
        if (_canGenerate && _collector.childCount < _enemyCount)
        {
            StartCoroutine(AddEnemy());

            _canGenerate = false;
        }
    }

    private IEnumerator AddEnemy()
    {
        int idx = UnityEngine.Random.Range(0, _positions.Length - 1);

        yield return new WaitForSeconds(_generatingDelay);

        _enemy.transform.position = _positions[idx].position;

        Instantiate(_enemy, _collector);

        _canGenerate = true;
    }
}
