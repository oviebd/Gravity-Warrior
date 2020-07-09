using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractorBase : MonoBehaviour
{
   protected PlayerMovement playerMovement;

    private void Awake()
    {
        playerMovement = PlayerMovement.instance;
    }

    public void OuterCircleCollided(IMoveData playerAttractData)
    {
        playerMovement.SetData(playerAttractData);
        playerMovement.SetMoveState(PlayerMovement.movingState.TowardAPosition);
    }


}
