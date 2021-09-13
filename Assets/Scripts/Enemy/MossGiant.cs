using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossGiant : Enemy, IDamageable
{
    public int Health { get; set; }

    protected override void Start()
    {
        base.Start();
        Health = _health;
    }

    protected override void Update()
    {
        if (_isDead == false)
        {
            Movement();
        }
    }

    public void Damage()
    {
        Health--;
        _isHit = true;
        if (_anim != null)
        {
            _anim.SetTrigger("Hit");
            _anim.SetBool("InCombat", true);
        }

        if (Health <= 0)
        {
            _isDead = true;
            Health = 0;
            //Debug.Log(this.name + " is dead!");
            transform.GetComponent<Collider2D>().enabled = false;
            StartCoroutine(DeathRoutine());
            SpawnGem();
        }
    }

    private IEnumerator DeathRoutine()
    {
        _anim.SetBool("Dead", true);
        yield return new WaitForSeconds(2f);
        Destroy(transform.parent.gameObject);
    }
}
