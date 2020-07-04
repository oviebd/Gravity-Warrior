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
}
