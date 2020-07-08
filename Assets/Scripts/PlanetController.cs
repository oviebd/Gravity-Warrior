using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    GameObject player;
    PlayerMovement playerMovement;

    public bool isItPocketType = false;
    public bool isPocketAutoShootable = true;

    [SerializeField] private GameObject center;
 
    [SerializeField] private float _playerAngularSpeed = 150.0f;
    [SerializeField] private float _playerMovingSpeed = 10.0f;
    [SerializeField] private float _playerRotationSpeed = 200.0f;

    private PlanetData planetData = new PlanetData();

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement =  player.GetComponent<PlayerMovement>();

        SetPlanetData();
    }

    public void OuterCircleCollidedWithPlayer()
    {
        playerMovement.ResetData();
        playerMovement.SetData(planetData, center);
        playerMovement.SetMoveState(PlayerMovement.movingState.TowardAPosition);
        
    }

    public void InnerCircleCollidedWithPlayer()
    {
       
        playerMovement.SetData(planetData, center);

        if (isItPocketType)
        {
            if (isPocketAutoShootable == true)
            {
                playerMovement.StopWithUpwordDirection();
                playerMovement.SetMoveState(PlayerMovement.movingState.pocketShoot);
            }
            else
                playerMovement.SetMoveState(PlayerMovement.movingState.StopWithUpwordDirection);
                //playerMovement.currentMovingState = PlayerMovement.movingState.StopWithUpwordDirection;

        }
        else
        {
            playerMovement.SetMoveState(PlayerMovement.movingState.RotateAround);
        }
    }


    private void SetPlanetData()
    {
        planetData = new PlanetData();
       
        planetData.centerObject = center;
        planetData.playerTorque = _playerRotationSpeed;
        planetData.playerMovingSpeed = _playerMovingSpeed;
        planetData.playerRotationSpeed = _playerAngularSpeed;
    }


}
