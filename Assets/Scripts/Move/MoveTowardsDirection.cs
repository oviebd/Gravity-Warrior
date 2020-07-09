using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsDirection : MoveBase,IMove
{
    public void SetUp(IMoveData data)
    {
        _moveData = data;
        _rb = GetRigidBody2D();
    }

    private void Update()
    {
        if (CanMove() == false)
            return;

        Move();
    }

    private void Move()
    {
        if (_moveData.direction == null)
            return;

        _rb.velocity = transform.up * _moveData.movingSpeed;
    }


}
