using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRingBase : MonoBehaviour
{
    
    protected IMoveData _playerAttractData;
    protected IAttractor _planetAttractor;

    private void Awake()
    {
        SetPlanetData();
        _planetAttractor = this.gameObject.GetComponentInParent<IAttractor>();
    }


    private void SetPlanetData()
    {
        _playerAttractData = new MoveData();
      /*  _playerAttractData.targetObj = null;
        _playerAttractData.movingSpeed = _playerMovingSpeed;
        _playerAttractData.torque = _playerTorque;
        _playerAttractData.rotationSpeed = 0.0f;*/
    }

}
