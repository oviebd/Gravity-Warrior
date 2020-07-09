using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttractor
{
    void InnerCircleCollided(IMoveData playerAttractData);
    void OuterCircleCollided(IMoveData playerAttractData);
}
