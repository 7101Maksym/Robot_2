using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Roofsvisible : MonoBehaviour
{
    private Tilemap _tilemap;

    private bool _isPlayerOnRoof = false;

    private void Awake()
    {
        _tilemap = GetComponent<Tilemap>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isPlayerOnRoof = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isPlayerOnRoof = false;
        }
    }

    private void Update()
    {
        if (_isPlayerOnRoof)
        {
            if (_tilemap.color.a > 0.2f)
            {
                _tilemap.color = new Color(1f, 1f, 1f, _tilemap.color.a - 0.01f);
            }
        }
        else
        {
            if (_tilemap.color.a < 1f)
            {
                _tilemap.color = new Color(1f, 1f, 1f, _tilemap.color.a + 0.01f);
            }
        }
    }
}
