using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Enemy : MonoBehaviour
{
    [SerializeField]
    protected int _health = 2;
    [SerializeField]
    protected float _speed = 2f;
    [SerializeField]
    protected int _gems = 1;
    [SerializeField]
    protected Transform _pointA = null, _pointB = null;

    protected Transform _currentPoint = null;
    protected Animator _anim = null;
    protected SpriteRenderer _sprite = null;

    protected virtual void Start()
    {
        _currentPoint = _pointA;

        _anim = GetComponentInChildren<Animator>();
        if (_anim == null)
        {
            Debug.LogError(this.name + " Animator is NULL");
        }

        _sprite = GetComponentInChildren<SpriteRenderer>();
        if (_sprite == null)
        {
            Debug.LogError(this.name + " Sprite Renderer is NULL");
        }
    }

    protected abstract void Update();

    protected virtual void Movement()
    {
        if (_anim != null)
        {
            if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                return;
            }
        }

        float distance = Vector3.Distance(transform.position, _currentPoint.position);

        if (distance <= 0.1f)
        {
            if (_currentPoint == _pointB)
            {
                _currentPoint = _pointA;
                Flip();
            }
            else
            {
                _currentPoint = _pointB;
                Flip();
            }

            if (_anim != null)
            {
                _anim.SetTrigger("Idle");
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _currentPoint.position, _speed * Time.deltaTime);
        }
    }

    protected virtual void Flip()
    {
        if (_currentPoint.position.x > transform.position.x)
        {
            if (_sprite != null)
            {
                if (_sprite.flipX != false)
                {
                    _sprite.flipX = false;                
                }
            }
        }
        else if (_currentPoint.position.x < transform.position.x)
        {
            if (_sprite != null)
            {
                if (_sprite.flipX != true)
                {
                    _sprite.flipX = true;
                }
            }
        }
    }
}
