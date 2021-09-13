using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy, IDamageable
{
    [SerializeField]
    private GameObject _acidPrefab = null;
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
            Flip(_player.transform);
        }
    }

    public void Damage()
    {
        Health--;

        if (Health <= 0)
        {
            _isDead = true;
            Health = 0;
            Debug.Log(this.name + " is dead!");
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

    public void Attack()
    {
        if (_acidPrefab != null)
        {
            GameObject obj = Instantiate(_acidPrefab, transform.position, Quaternion.identity);
            AcidEffect acidScript = obj.GetComponent<AcidEffect>();
            if (acidScript != null)
            {
                acidScript.SetMoveDirection(_sprite.flipX);
            }
        }
    }
}
