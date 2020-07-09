using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetOuterRing : PlanetRingBase, ICollisionEnter
{
    [SerializeField] private GameObject _nextTarget;
    [SerializeField] private float _playerMovingSpeed = 10.0f;
    [SerializeField] private float _playerTorque = 200.0f;

    private void Start()
    {
        _playerAttractData.targetObj = _nextTarget;
        _playerAttractData.movingSpeed = _playerMovingSpeed;
        _playerAttractData.torque = _playerTorque;
    }

    public void onCollisionEnter(GameObject collidedObj, GameObject selfObj)
    {
        if (_planetAttractor != null)
            _planetAttractor.OuterCircleCollided(_playerAttractData);
    }
}
