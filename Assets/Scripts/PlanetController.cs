using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    GameObject player;
    PlayerMovement playerMovement;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement =  player.GetComponent<PlayerMovement>();
    }

    public void OuterCircleCollidedWithPlayer()
    {
        playerMovement.currentMovingState = PlayerMovement.movingState.TowardAPosition;
    }

    public void InnerCircleCollidedWithPlayer()
    {
        playerMovement.currentMovingState = PlayerMovement.movingState.RotateAround;
    }


}
