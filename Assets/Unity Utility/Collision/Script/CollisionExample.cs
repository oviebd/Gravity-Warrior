using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionExample : MonoBehaviour, ICollisionEnter
{
    public void onCollisionEnter(GameObject collidedObj, GameObject selfObj)
    {
        Debug.Log("Collision Enter - " + collidedObj.name);
    }
}
