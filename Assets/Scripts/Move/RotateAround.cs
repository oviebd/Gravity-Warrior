using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MoveBase,IMove
{
    private float alpha = 0.0f;
    private bool _isClockWiseMove;

    public void SetUp(MovingData data)
    {
        _moveData = data;
        _rb = GetRigidBody2D();

        alpha = Utils.angle360(_moveData.targetObj.transform.position, this.transform.position);
        _isClockWiseMove = Utils.IsClockwiseMoveOrNot(_moveData.targetObj.transform, this.transform);
    }

   
    private void Update()
    {
        //Debug.Log("Rotate Please... before if");
        if ( _canMove == false || _rb == null || _moveData.targetObj == null)
            return;
       // Debug.Log("Rotate Please... after If ");
        MovingAroundATarget();
        RotateObject();
    }

    void MovingAroundATarget()
    {
        float rad = alpha * Mathf.Deg2Rad;
        float radious = Vector2.Distance(this.transform.position, _moveData.targetObj.transform.position);

        float posX =  _moveData.targetObj.transform.position.x + Mathf.Cos(rad) * radious;
        float posY = _moveData.targetObj.transform.position.y + Mathf.Sin(rad) * radious;

        transform.position = new Vector2(posX, posY);

        if (_isClockWiseMove == true)
            alpha = alpha - Time.deltaTime * _moveData.rotationSpeed;
        else
            alpha = alpha + Time.deltaTime * _moveData.rotationSpeed;

       // Debug.Log("radious : " + radious + "  alpha is  :  " + alpha + " Clocklwisw " + _isClockWiseMove);

    }

    private void RotateObject()
    {
        Vector3 direction;

        if (_isClockWiseMove == true)
            direction = _moveData.targetObj.transform.position - transform.position;
        else
            direction = transform.position - _moveData.targetObj.transform.position;


        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }


   
}
