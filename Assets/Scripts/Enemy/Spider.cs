using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy, IDamageable
{
    public int Health { get => _health; set => _health = value; }

    protected override void Update()
    {
        Movement();
    }

    public void Damage()
    {
        Health--;
        if (Health <= 0)
        {
            Health = 0;
            Debug.Log(this.name + " is dead!");
            Destroy(transform.parent.gameObject);
        }
    }
}
