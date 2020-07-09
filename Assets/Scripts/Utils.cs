using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils 
{
	public static float angle360(Vector2 fixedPosition, Vector2 movingObjectPosition)
	{
		Vector2 direction = movingObjectPosition - fixedPosition;
		//Vector2 directionRoot = (new Vector2(fixedPosition.x * 2, fixedPosition.y)) - fixedPosition ;
		Vector2 directionRoot;
        if (fixedPosition.x < 0)
        {
			directionRoot = (new Vector2(fixedPosition.x * (-2), fixedPosition.y)) - fixedPosition;
		}
        else
        {
            directionRoot = (new Vector2(fixedPosition.x * 2, fixedPosition.y)) - fixedPosition;
		}

		direction.Normalize();
		directionRoot.Normalize();
		float sign = (direction.y >= 0) ? 1 : -1;
		float offset = (sign >= 0) ? 0 : 360;
		float angle =( Vector3.Angle(directionRoot, direction) * sign) + offset;
		return angle;
	}

	public static float GetDistance(Vector2 obj1, Vector2 obj2)
	{
		float distance = Vector3.Distance(obj1, obj2);
		return distance;
	}


    public static Vector2 GetPlayerDirection(Vector2 lastPosition, Vector2 currentPosition,Transform player)
    {
 		Vector2 direction = currentPosition - lastPosition;
		var localDirection = player.InverseTransformDirection(direction);

		localDirection.Normalize();
 		return localDirection;
	}

    public static float GetRotateAmountBetweenTwoObject(Transform targetTransform,Transform playerTransform)
    {
		Vector3 direction = targetTransform.position - playerTransform.position;
		direction.Normalize();
		float rotateAmount = Vector3.Cross(direction, playerTransform.up).z;

		return rotateAmount;
	}

    public static bool IsClockwiseMoveOrNot(Transform targetTransform, Transform playerTransform)
    {
		float rotateAmount = GetRotateAmountBetweenTwoObject(targetTransform,playerTransform);

        if (rotateAmount >= 0)
			return true;
		else
			return false;
	}

    public static IMove[] GetAllImoveObjectFromAobject(GameObject obj)
    {
		IMove[] iMoveList = obj.gameObject.GetComponents<IMove>();
		return iMoveList;
    }

    public static void StopOrStartMovementOfAobj(GameObject obj,bool canMove)
    {
		IMove[] iMoveList = GetAllImoveObjectFromAobject(obj);
        if(iMoveList != null && iMoveList.Length > 0)
        {
            for(int i=0; i<iMoveList.Length; i++)
            {
                if(canMove == true)
					iMoveList[i].StartMove();
                else
					iMoveList[i].StopMove();
			}
        }

	}

   public static GameObject GetGameobjectsWithinARadious(float range, string tag,GameObject centerObj)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(centerObj.transform.position, range);
        float minDistance = Mathf.Infinity;
        GameObject closeObject = null;
        Collider2D playerCollider = centerObj.gameObject.GetComponent<Collider2D>();
        for (int i = 0; i < colliders.Length; i++)
        {
            GameObject obj = colliders[i].gameObject;


            if (playerCollider != colliders[i] && obj.tag == tag)
            {
                float distance = Vector2.Distance(centerObj.transform.position, obj.transform.position);
                if (distance < minDistance)
                {
                    closeObject = obj;
                    minDistance = distance;
                }
            }
        }
        return closeObject;
    }

}
