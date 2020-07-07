using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingData
{
    public float movingSpeed = 0.0f;
    public float rotationSpeed = 0.0f;
    public float torque = 0.0f;
    public Vector3 direction = Vector3.up;
    public GameObject targetObj = null;
}
