using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherAttractorController : AttractorBase, IAttractor
{
    [SerializeField] private bool _isAutoLaunch = false;

    [Header("Player Speed")]
    [SerializeField] float _launchSpeed = 50.0f;

    public void InnerCircleCollided(IMoveData playerAttractData)
    {
        playerMovement.ResetData();

        if (_isAutoLaunch == true)
        {
            playerAttractData.movingSpeed = _launchSpeed;
            playerMovement.SetData(playerAttractData);
            playerMovement.StopWithUpwordDirection();
            playerMovement.SetMoveState(PlayerMovement.movingState.pocketShoot);
        }
        else
        {
            playerAttractData.movingSpeed = 0.0f;
            playerMovement.SetData(playerAttractData);
            playerMovement.SetMoveState(PlayerMovement.movingState.StopWithUpwordDirection);
        }
            
    }
    
}
