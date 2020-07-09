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
    }

}
