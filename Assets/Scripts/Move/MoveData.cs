using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveData : IMoveData
{
    public float movingSpeed { get; set; }
    public float rotationSpeed { get; set; }
    public float torque { get; set; }
    public Vector3 direction { get; set; }
    public GameObject targetObj { get ; set; }

    public IMoveData DeepCopy(IMoveData data)
    {
        IMoveData copyData = new MoveData();

        copyData.movingSpeed = data.movingSpeed;
        copyData.rotationSpeed = data.rotationSpeed;
        copyData.torque = data.torque;
        copyData.direction = data.direction;
        copyData.targetObj = data.targetObj;
        
        return copyData;
    }
}
