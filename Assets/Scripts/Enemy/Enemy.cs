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

    protected bool _isDead = false;
    protected bool _isHit = false;
    [SerializeField]
    protected float _combatDistance = 2f;
    protected Player _player = null;
    protected Transform _hitboxTrans = null;

    protected virtual void Start()
    {
        _currentPoint = _pointA;

        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError(this.name + ": Player is NULL");
        }

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
        else
        {
            if (_sprite.transform.childCount > 0)
            {
                _hitboxTrans = _sprite.transform.GetChild(0);
                if (_hitboxTrans == null)
                {
                    Debug.LogError(this.name + " Hitbox Transform is NULL");
                }
            }
        }
    }

    protected abstract void Update();

    protected virtual void Movement()
    {
        if (_anim != null)
        {
            if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && _anim.GetBool("InCombat") == false)
            {
                return;
            }
        }

        float distance = Vector2.Distance(transform.position, _currentPoint.position);

        if (distance <= 0.1f)
        {
            if (_currentPoint == _pointB)
            {
                _currentPoint = _pointA;
                Flip(_currentPoint);
            }
            else
            {
                _currentPoint = _pointB;
                Flip(_currentPoint);
            }

            if (_anim != null)
            {
                _anim.SetTrigger("Idle");
            }
        }
        else if (_isHit == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, _currentPoint.position, _speed * Time.deltaTime);
        }
        else
        {
            if (_player != null)
            {
                Flip(_player.transform);
                float playerDistance = Vector2.Distance(_player.transform.position, transform.position);
                if (playerDistance > _combatDistance)
                {
                    _isHit = false;
                    if (_anim != null)
                    {
                        _anim.SetBool("InCombat", false);
                    }
                }
            }
        }
    }

    protected virtual void Flip(Transform target)
    {
        if (target.position.x > transform.position.x)
        {
            if (_sprite != null)
            {
                if (_sprite.flipX != false)
                {
                    _sprite.flipX = false;
                    if (_hitboxTrans != null)
                    {
                        _hitboxTrans.localPosition = new Vector3(-_hitboxTrans.localPosition.x, 0);
                    }
                }
            }
        }
        else if (target.position.x < transform.position.x)
        {
            if (_sprite != null)
            {
                if (_sprite.flipX != true)
                {
                    _sprite.flipX = true;
                    if (_hitboxTrans != null)
                    {
                        _hitboxTrans.localPosition = new Vector3(-_hitboxTrans.localPosition.x, 0);
                    }
                }
            }
        }
    }

    public void PlayerNearby(bool nearby)
    {
        if (_anim != null)
        {
            _anim.SetBool("InCombat", nearby);
        }
    }
}
