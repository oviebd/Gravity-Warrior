using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    public float movingSpeed = 5.0f;
    public float rotateSpeed = 200.0f;
    private Transform target;

    public enum movingState { MoveUp, TowardAPosition, RotateAround ,ForceTowardsADirection};

    public movingState currentMovingState = movingState.MoveUp;

 
   public float angularSpeed = 20.0f;
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
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentMovingState = movingState.ForceTowardsADirection;
        }
    }

    public void PlayerNormalMove()
    {
        if (lastPosition == null)
            lastPosition = this.transform.position;
        rb.velocity = transform.up * movingSpeed;
    }

    public void MoveTowardsAposition()
    {
        Vector2 direction = (Vector2)target.position - rb.position;
        direction.Normalize();
        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        rb.angularVelocity = -rotateAmount * rotateSpeed;
        rb.velocity = transform.up * movingSpeed;
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

            Vector2 rocketDirection = Utils.GetPlayerDirection(lastPosition, this.gameObject.transform.position,this.transform);

            if ((alpha >= 0 && alpha <= 90) || (alpha >= 270 && alpha <= 360))
            {
                isClockwiseMove = false;
            }
            else
                isClockwiseMove = true;
	
           Debug.Log("init Angle is : " + alpha + " isClock Move  " + isClockwiseMove);	
		}

		float rad = alpha * Mathf.Deg2Rad;

        posX = _planetData.centerObject.transform.position.x + Mathf.Cos(rad) * _planetData.radious;
		posY = _planetData.centerObject.transform.position.y + Mathf.Sin(rad) * _planetData.radious;

		transform.position = new Vector2(posX, posY);
        if(isClockwiseMove == true)
            alpha = alpha - Time.deltaTime * angularSpeed;
        else
            alpha = alpha + Time.deltaTime * angularSpeed;

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



}
