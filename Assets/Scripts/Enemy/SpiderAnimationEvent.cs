using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpiderAnimationEvent : MonoBehaviour
{
    [SerializeField]
    private GameObject _acidPrefab = null;
    private SpriteRenderer _sprite = null;

    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    public void Fire()
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
