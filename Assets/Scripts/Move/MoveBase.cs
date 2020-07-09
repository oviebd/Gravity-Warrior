using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBase : MonoBehaviour
{
    protected bool _canMove = false;
    protected  IMoveData _moveData = new  MoveData();
    protected Rigidbody2D _rb;

    void Start()
    {
        GetRigidBody2D();
    }

    public void StartMove()
    {
        StopMovement();
        _canMove = true;
    }

    public void StopMove()
    {
        _canMove = false;
        StopMovement();
    }

    protected Rigidbody2D GetRigidBody2D()
    {
        if( this.gameObject.GetComponent<Rigidbody2D>() != null)
        {
           _rb = this.gameObject.GetComponent<Rigidbody2D>();
            return _rb;
        }
        return null;
    }

    protected void StopMovement()
    {
        if (_rb != null)
        {
            _rb.velocity = Vector2.zero;
            _rb.angularVelocity = 0.0f;
        }
    }

    protected bool CanMove()
    {
        if (_canMove == false || _rb == null || _moveData == null)
            return false;
        else
           return true;
    }
}
