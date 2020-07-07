using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMove 
{
    void StartMove();
    void SetUp(MovingData data);
    void StopMove();
}
