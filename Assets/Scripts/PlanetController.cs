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
    [SerializeField] private float radious = 1.0f;
    [SerializeField] private float _playerAngularSpeed = 150.0f;
    [SerializeField] private float _playerMovingSpeed = 10.0f;
    [SerializeField] private float _playerRotationSpeed = 200.0f;

    private PlanetData planetData = new PlanetData();


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement =  player.GetComponent<PlayerMovement>();

        SetPlanetData();
    }

    public void OuterCircleCollidedWithPlayer()
    {
        playerMovement.ResetData();
        playerMovement.currentMovingState = PlayerMovement.movingState.TowardAPosition;
        playerMovement.SetData(planetData, center);
        
    }

    public void InnerCircleCollidedWithPlayer()
    {
        playerMovement.ResetData();
        playerMovement.SetData(planetData, center);
        if (isItPocketType)
        {
            playerMovement.currentMovingState = PlayerMovement.movingState.StopWithUpwordDirection;
            if(isPocketAutoShootable == true)
            {
                playerMovement.currentMovingState = PlayerMovement.movingState.pocketShoot;
            }

        }
        else
        {
            playerMovement.currentMovingState = PlayerMovement.movingState.RotateAround;
        }
    }


    private void SetPlanetData()
    {
        planetData = new PlanetData();
        planetData.radious = radious;
        planetData.centerObject = center;
        planetData.playerRotationSPeed = _playerRotationSpeed;
        planetData.playerMovingSpeed = _playerMovingSpeed;
        planetData.playerAngularSpeed = _playerAngularSpeed;
    }


}
