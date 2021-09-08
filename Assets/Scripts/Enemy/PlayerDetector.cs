using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class PlayerDetector : MonoBehaviour
{
    private Enemy _enemy = null;

    void Start()
    {
        _enemy = GetComponentInChildren<Enemy>();
        if (_enemy == null)
        {
            Debug.LogError("Enemy is Null");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (_enemy != null)
            {
                _enemy.PlayerNearby(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (_enemy != null)
            {
                _enemy.PlayerNearby(false);
            }
        }
    }
}
