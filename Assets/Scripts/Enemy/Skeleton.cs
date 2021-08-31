using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy, IDamageable
{
    public int Health { get => _health; set => _health = value; }

    protected override void Update()
    {
        Movement();
    }

    public void Damage()
    {
        Health--;
        _anim.SetTrigger("Hit");
        if (Health <= 0)
        {
            Health = 0;
            Debug.Log(this.name + " is dead!");
            Destroy(transform.parent.gameObject);
        }
    }
}
