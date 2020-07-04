using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    GameObject player;
    PlayerMovement playerMovement;

    [SerializeField] private GameObject center;
    [SerializeField] private float radious = 1.0f;

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
        playerMovement.currentMovingState = PlayerMovement.movingState.RotateAround;

    }


    private void SetPlanetData()
    {
        planetData = new PlanetData();
        planetData.radious = radious;
        planetData.centerObject = center;
    }


}
