using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _anim = null;
    private Animator _swordArcAnim = null;

    void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        if (_anim == null)
        {
            Debug.LogError("Player Animator is NULL");
        }

        _swordArcAnim = transform.GetChild(1).GetComponent<Animator>();
        if (_swordArcAnim == null)
        {
            Debug.LogError("Sword Arc Animator is NULL");
        }
    }

    public void Move(float speed)
    {
        if (_anim != null)
        {
            _anim.SetFloat("Speed", speed);
        }
    }

    public void Jumping(bool jumping)
    {
        if (jumping == true)
        {
            _anim.SetBool("Jump", true);
        }
        else
        {
            _anim.SetBool("Jump", false);
        }
    }

    public void Attack()
    {
        if (_anim != null)
        {
            _anim.SetTrigger("Attack");
        }

        if (_swordArcAnim != null)
        {
            _swordArcAnim.SetTrigger("Swing");
        }
    }

    public void Death()
    {
        if (_anim != null)
        {
            _anim.SetTrigger("Dead");
        }
    }
}
