using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsTarget : MoveBase, IMove
{

    public void SetUp(IMoveData data)
    {
        _moveData = data;
        _rb = GetRigidBody2D();
//        Debug.Log("Rmov speed ... " + _moveData.movingSpeed + "  center obj is : " + _moveData.targetObj);
    }

    private void Update()
    {
        if ( CanMove() == false)
            return;

        MoveTowardsATarget();
    }

    private void MoveTowardsATarget()
    {
        if (_moveData.targetObj == null)
            return;

        float rotateAmount = Utils.GetRotateAmountBetweenTwoObject(_moveData.targetObj.transform, this.gameObject.transform);
        _rb.angularVelocity = -rotateAmount * _moveData.torque;
        _rb.velocity = transform.up * _moveData.movingSpeed;
    }

    
}
