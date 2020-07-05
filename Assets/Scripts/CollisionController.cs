using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionController : MonoBehaviour
{
    public UnityEvent onCollide;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (onCollide != null)
            onCollide.Invoke();
    }
}
