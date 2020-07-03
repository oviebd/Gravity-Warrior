using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils 
{
	public static float GetAngle(Vector3 target, Transform otherObj)
	{
		Vector2 playerRight = Vector2.up;
		float towardsOther = (otherObj.position.y - playerRight.y) / (otherObj.position.x - playerRight.x);
		float angle = Mathf.Atan(towardsOther) * Mathf.Rad2Deg;
		angle = Mathf.Abs(angle);
		int quard = GetQuardLocation(otherObj.position) - 1;

		if (quard == 3)
			angle = 360 - angle;
		else if (quard == 1)
			angle = 180 - angle;
		else
			angle = (quard * 90) + angle;

		return angle;
	}

	private static int GetQuardLocation(Vector3 point)
	{
		int quardNumber = 1;
		if (point.x > 0 && point.y > 0)
			quardNumber = 1;
		else if (point.x > 0 && point.y < 0)
			quardNumber = 4;
		else if (point.x < 0 && point.y > 0)
			quardNumber = 2;
		else if (point.x < 0 && point.y < 0)
			quardNumber = 3;

		return quardNumber;
	}
}
