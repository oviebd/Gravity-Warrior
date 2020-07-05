using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    public float movingSpeed = 5.0f;
   
    private Transform target;

    public enum movingState { MoveUp, TowardAPosition, RotateAround ,ForceTowardsADirection,StopWithUpwordDirection,pocketShoot};

    public movingState currentMovingState = movingState.MoveUp;

 
  
   float posX, posY = 0.0f;
   float alpha = 0.0f;

    private PlanetData _planetData;

    bool isRotationStart = false;
    bool isdirectedForceStart = false;
    Vector2 lastPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        currentMovingState = movingState.MoveUp;
        lastPosition = this.transform.position;
    }

    public void SetData(PlanetData planetData, GameObject targetObj)
    {
        target = targetObj.transform;
        _planetData = planetData;
        isRotationStart = false;
        isdirectedForceStart = false;
    }

    public void ResetData()
    {
        target = null;
        _planetData = null;
    }
   
    void Update()
    {
        //Debug.Log("Current State : " + currentMovingState);
       switch (currentMovingState)
        {
            case movingState.MoveUp:
                PlayerNormalMove();
                break;
           case movingState.TowardAPosition:
                MoveTowardsAposition();
                break;
           case movingState.RotateAround:
               MoveRotateARound();
               break;
           case movingState.ForceTowardsADirection:
                ForceTowardsADirection();
                break;
            case movingState.StopWithUpwordDirection:
                StopWithUpwordDirection();
                break;
            case movingState.pocketShoot:
                ShootFromPocket();
                break;

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentMovingState = movingState.ForceTowardsADirection;
        }
    }

    public void PlayerNormalMove()
    {
        unparentRocket();
        rb.velocity = transform.up * movingSpeed;
    }
    public void PlayerNormalMove(float speed)
    {
        unparentRocket();
        rb.velocity = transform.up * speed;
    }

    public void MoveTowardsAposition()
    {
        Vector2 direction = (Vector2)target.position - rb.position;
        direction.Normalize();
        float rotateAmount = Vector3.Cross(direction, transform.up).z;


        if (rotateAmount >= 0)
            isClockwiseMove = true;
        else
            isClockwiseMove = false;

        rb.angularVelocity = -rotateAmount * _planetData.playerRotationSPeed;
        rb.velocity = transform.up * _planetData.playerMovingSpeed;
    }

    bool isClockwiseMove = true;

    public void MoveRotateARound()
    {
        if (target == null || _planetData == null)
            return;


		rb.velocity = Vector2.zero;
       // rb.angularVelocity = 0.0f;

        if (isRotationStart == false)
        {
            isRotationStart = true;
            alpha = Utils.angle360(target.position, this.gameObject.transform.position);
		}

		float rad = alpha * Mathf.Deg2Rad;

        posX = _planetData.centerObject.transform.position.x + Mathf.Cos(rad) * _planetData.radious;
		posY = _planetData.centerObject.transform.position.y + Mathf.Sin(rad) * _planetData.radious;

		transform.position = new Vector2(posX, posY);
        if(isClockwiseMove == true)
            alpha = alpha - Time.deltaTime * _planetData.playerAngularSpeed;
        else
            alpha = alpha + Time.deltaTime * _planetData.playerAngularSpeed;

        Vector3 dir;
        if(isClockwiseMove == true)
            dir = target.position - transform.position;
        else
            dir = transform.position  - target.position;


        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis( angle, Vector3.forward);

    }

    void ForceTowardsADirection()
    {
        if(isdirectedForceStart == false)
        {
            isdirectedForceStart = true;
            rb.angularVelocity = 0.0f;
            lastPosition = this.transform.position;
        }
        PlayerNormalMove();
    }


    public void StopWithUpwordDirection()
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0.0f;
        transform.rotation = new Quaternion(0, 0, 0, 0);
        transform.parent = target;
    }

    public void ShootFromPocket()
    {
        StopWithUpwordDirection();
        PlayerNormalMove(40.0f);
    }

    void unparentRocket()
    {
        transform.parent = null;
    }

}
