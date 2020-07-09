using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractorBase : MonoBehaviour
{
   protected PlayerMovement playerMovement;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    public void OuterCircleCollided(IMoveData playerAttractData)
    {
        playerMovement.ResetData();
        playerMovement.SetData(playerAttractData);
        playerMovement.SetMoveState(PlayerMovement.movingState.TowardAPosition);
    }


}
