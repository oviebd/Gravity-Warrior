using UnityEngine;

public class PlanetInnerRing : PlanetRingBase, ICollisionEnter
{
    [SerializeField] private float _playerRotationSpeed = 150.0f;

    private void Start()
    {
        _playerAttractData.targetObj = this.gameObject;
        _playerAttractData.rotationSpeed = _playerRotationSpeed;
    }


    public void onCollisionEnter(GameObject collidedObj, GameObject selfObj)
    {
        if (_planetAttractor != null)
            _planetAttractor.InnerCircleCollided(_playerAttractData);
    }
}
