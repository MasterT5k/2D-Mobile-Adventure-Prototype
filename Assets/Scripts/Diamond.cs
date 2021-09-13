using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class Diamond : MonoBehaviour
{
    [SerializeField]
    private int _value = 1;

    public void SetValue(int value)
    {
        _value = value;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.UpdateGems(_value);
            }

            Destroy(gameObject);
        }
    }
}
