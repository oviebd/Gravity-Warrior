using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsTarget : MoveBase, IMove
{

    public void SetUp(MovingData data)
    {
        _moveData = data;
        _rb = GetRigidBody2D();
    }

    private void Update()
    {
        if ( _canMove == false || _rb == null || _moveData.targetObj == null)
            return;

        MoveTowardsATarget();
    }

    private void MoveTowardsATarget()
    {
        float rotateAmount = Utils.GetRotateAmountBetweenTwoObject(_moveData.targetObj.transform, this.gameObject.transform);
        _rb.angularVelocity = -rotateAmount * _moveData.torque;
        _rb.velocity = transform.up * _moveData.movingSpeed;
    }

}
