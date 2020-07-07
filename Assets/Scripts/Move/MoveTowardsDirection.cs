using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsDirection : MoveBase,IMove
{
    public void SetUp(MovingData data)
    {
        _moveData = data;
        _rb = GetRigidBody2D();
    }

    private void Update()
    {
        if (_canMove == false || _rb == null && _moveData.direction == null)
            return;

        Move();
    }

    private void Move()
    {
        _rb.velocity = transform.up * _moveData.movingSpeed;
    }


}
