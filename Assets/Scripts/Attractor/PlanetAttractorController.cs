using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetAttractorController : AttractorBase,IAttractor
{
    public void InnerCircleCollided(IMoveData playerAttractData)
    {
        playerMovement.ResetData();
        playerMovement.SetData(playerAttractData);
        playerMovement.SetMoveState(PlayerMovement.movingState.RotateAround);
    }
}
