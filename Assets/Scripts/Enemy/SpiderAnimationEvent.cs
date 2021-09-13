using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpiderAnimationEvent : MonoBehaviour
{
    private Spider _spider = null;

    private void Start()
    {
        _spider = GetComponentInParent<Spider>();
        if (_spider == null)
        {
            Debug.LogError("Spider script is NULL");
        }
    }

    public void Fire()
    {
        if (_spider != null)
        {
            _spider.Attack();
        }
    }
}
