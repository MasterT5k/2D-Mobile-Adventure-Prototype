using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Player : MonoBehaviour, IDamageable
{
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private float _jumpForce = 5f;
    [SerializeField]
    private LayerMask _groundLayer;
    private Rigidbody2D _rb = null;
    private PlayerAnimation _animScript = null;
    private SpriteRenderer _spriteRenderer = null;
    private SpriteRenderer _swordArcRenderer = null;
    private Transform _hitboxTrans = null;
    private bool _jumping = false;
    private bool _attacking = false;
    [SerializeField]
    private float _attackDelay = 0.875f;
    [SerializeField]
    private int _startingHealth = 5;
    private bool _isDead = false;
    public int Health { get; set; }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animScript = GetComponent<PlayerAnimation>();
        if (_animScript == null)
        {
            Debug.LogError("PlayerAnimation script is NULL");
        }

        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (_spriteRenderer == null)
        {
            Debug.LogError("Player Sprite is NULL");
        }
        else
        {
            if (_spriteRenderer.transform.childCount > 0)
            {
                _hitboxTrans = _spriteRenderer.transform.GetChild(0);
            }

            if (_hitboxTrans == null)
            {
                Debug.LogError("Hitbox Transform is NULL");
            }
        }

        _swordArcRenderer = transform.GetChild(1).GetComponent<SpriteRenderer>();
        if (_swordArcRenderer == null)
        {
            Debug.LogError("Sword Arc Renderer is NULL");
        }

        Health = _startingHealth;
    }

    void Update()
    {
        if (_isDead == true)
        {
            return;
        }

        CalulateMovement();
        if (_jumping == true)
        {
            if (CheckGrounded() == true)
            {
                _jumping = false;
                _animScript.Jumping(false);
            }
        }

        if (Input.GetMouseButtonDown(0) && CheckGrounded() == true && _attacking == false)
        {
            _animScript.Attack();
            _attacking = true;
            StartCoroutine(AttackDelay());
        }
    }

    void CalulateMovement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        Vector2 velocity = new Vector2(horizontalInput * _speed, _rb.velocity.y);

        if (_animScript != null)
        {
            _animScript.Move(Mathf.Abs(horizontalInput));
        }

        if (Input.GetKeyDown(KeyCode.Space) && CheckGrounded())
        {
            velocity.y = _jumpForce;
            _animScript.Jumping(true);
            StartCoroutine(JumpCheckDelay());
        }

        if (_rb != null)
        {
            _rb.velocity = velocity;
        }

        Flip(horizontalInput);
    }

    void Flip(float horizontalInput)
    {
        if (horizontalInput > 0)
        {
            if (_spriteRenderer != null)
            {
                if (_spriteRenderer.flipX != false)
                {
                    _spriteRenderer.flipX = false;
                    _swordArcRenderer.flipX = false;
                    _hitboxTrans.localPosition = new Vector3(-_hitboxTrans.localPosition.x, 0);
                }
            }
        }
        else if (horizontalInput < 0)
        {
            if (_spriteRenderer != null)
            {
                if (_spriteRenderer.flipX != true)
                {
                    _spriteRenderer.flipX = true;
                    _swordArcRenderer.flipX = true;
                    _hitboxTrans.localPosition = new Vector3(-_hitboxTrans.localPosition.x, 0);
                }
            }
        }
    }

    private bool CheckGrounded()
    {
        if (Physics2D.Raycast(transform.position, Vector2.down, 0.75f, _groundLayer))
        {
            Debug.DrawRay(transform.position, Vector2.down, Color.green);
            return true;
        }
        return false;
    }

    IEnumerator JumpCheckDelay()
    {
        yield return new WaitForSeconds(0.1f);
        _jumping = true;
    }

    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(_attackDelay);
        _attacking = false;
    }

    public void Damage()
    {
        Health--;
        if (Health <= 0)
        {
            Health = 0;
            _isDead = true;
            Debug.Log(this.name + " is dead!");
            StartCoroutine(DeathRoutine());
        }
    }

    private IEnumerator DeathRoutine()
    {
        _animScript.Death();
        yield return new WaitForSeconds(2f);
        UnityEditor.EditorApplication.ExitPlaymode();
    }
}
