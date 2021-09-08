using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class AcidEffect : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private float _destroyDelay = 5f;
    private bool _moveLeft = false;

    void Start()
    {
        Destroy(gameObject, _destroyDelay);
    }

    void Update()
    {
        if (_moveLeft == false)
        {
            transform.Translate(Vector3.right * _speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.left * _speed * Time.deltaTime);
        }
    }

    public void SetMoveDirection(bool moveLeft)
    {
        _moveLeft = moveLeft;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.Damage();
            }
        }

        Destroy(gameObject);
    }
}
