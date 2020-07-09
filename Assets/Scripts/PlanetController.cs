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
 
    private float _playerAngularSpeed = 150.0f;
    private float _playerMovingSpeed = 10.0f;
    private float _playerRotationSpeed = 200.0f;

    private IMoveData _playerMoveData;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement =  player.GetComponent<PlayerMovement>();
        SetPlanetData();
    }

    public void OuterCircleCollidedWithPlayer()
    {
        playerMovement.ResetData();
       // SetPlanetData();
        playerMovement.SetData(_playerMoveData);
       // Debug.Log("PLANET DATA : SPEED : " + _playerMoveData.movingSpeed);
        playerMovement.SetMoveState(PlayerMovement.movingState.TowardAPosition);
        
    }

    public void InnerCircleCollidedWithPlayer()
    {
        playerMovement.ResetData();
       // SetPlanetData();
        playerMovement.SetData(_playerMoveData);

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
        _playerMoveData = new MoveData();
        _playerMoveData.targetObj = center;
        _playerMoveData.movingSpeed = _playerMovingSpeed;
        _playerMoveData.torque = _playerRotationSpeed;
        _playerMoveData.rotationSpeed = _playerAngularSpeed;
    }


}
