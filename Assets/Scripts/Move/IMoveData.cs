using UnityEngine;
using System.Collections;

public interface IMoveData
{
    float movingSpeed { get; set; }
    float rotationSpeed { get; set; }
    float torque { get; set; }
    Vector3 direction { get; set; }
    GameObject targetObj { get; set;}
    IMoveData DeepCopy(IMoveData data);
}
