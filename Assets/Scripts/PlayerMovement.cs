using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    public float movingSpeed = 5.0f;
   
    private Transform target;

    public enum movingState { MoveUp, TowardAPosition, RotateAround ,ForceTowardsADirection,StopWithUpwordDirection,pocketShoot,FreeJump};

    public movingState currentMovingState = movingState.MoveUp;

 
  
   float posX, posY = 0.0f;
   float alpha = 0.0f;

    private PlanetData _planetData;

    bool isRotationStart = false;
    bool isdirectedForceStart = false;

    bool isCloseObjectGet = false;
    GameObject closeObject = null;
    public bool isPlayerDocked = false;
   

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
        isCloseObjectGet = false;
        closeObject = null;
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

            case movingState.FreeJump:
                FreeJumpMove();
                break;

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(isPlayerDocked == true)
                currentMovingState = movingState.ForceTowardsADirection;
            else
                currentMovingState = movingState.FreeJump;

        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            
        }
    }

    public void SetMoveState(movingState state)
    {
        currentMovingState = state;
    }

    public void PlayerNormalMove()
    {
        PlayerNormalMove(movingSpeed);
    }
    public void PlayerNormalMove(float speed)
    {
        isPlayerDocked = false;
        unparentRocket();
        rb.velocity = transform.up * speed;
    }

    public void MoveTowardsAposition()
    {
        if (_planetData == null)
            return;
       // isPlayerDocked = true;
     
       MoveTowardsAposition(target, _planetData.playerRotationSPeed, _planetData.playerMovingSpeed);
       
    }

    public void MoveTowardsAposition(Transform target,float rotateSpeed,float movingSpeed)
    {
        
        Vector2 direction = (Vector2)target.position - rb.position;
        direction.Normalize();
        float rotateAmount = Vector3.Cross(direction, transform.up).z;


        if (rotateAmount >= 0)
            isClockwiseMove = true;
        else
            isClockwiseMove = false;

        rb.angularVelocity = -rotateAmount * rotateSpeed;
        rb.velocity = transform.up * movingSpeed;
    }

    bool isClockwiseMove = true;

    public void MoveRotateARound()
    {
        if (target == null || _planetData == null)
            return;

        isPlayerDocked = true;
     

        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0.0f;

        if (isRotationStart == false)
        {
            isRotationStart = true;
            alpha = Utils.angle360(target.position, this.gameObject.transform.position);
		}

		float rad = alpha * Mathf.Deg2Rad;
        float distance = Vector2.Distance(this.transform.position, _planetData.centerObject.transform.position);
        float properDistance = distance ;
        float radious = properDistance;
        Debug.Log("distance : " + distance + "  proper distance :  " + properDistance);

        posX = _planetData.centerObject.transform.position.x + Mathf.Cos(rad) * radious;
		posY = _planetData.centerObject.transform.position.y + Mathf.Sin(rad) * radious;

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
            
        }
        PlayerNormalMove();
    }


    public void StopWithUpwordDirection()
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0.0f;
        transform.rotation = new Quaternion(0, 0, 0, 0);
        transform.parent = target;
        transform.localPosition = Vector2.zero;
    }

    public void ShootFromPocket()
    {
        PlayerNormalMove(50.0f);
    }

    void unparentRocket()
    {
        transform.parent = null;
    }

   

    void FreeJumpMove()
    {

        if(isCloseObjectGet == false)
        {
            isCloseObjectGet = true;
            closeObject = GetGameobjectsWithinARadious(10.0f);
        }
        if (closeObject == null)
            return;

        MoveTowardsAposition(closeObject.transform,1000,30);

    }

    GameObject GetGameobjectsWithinARadious(float range)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(this.gameObject.transform.position, range);
        float minDistance = Mathf.Infinity;
        GameObject closeObject = null;
        Collider2D playerCollider = this.gameObject.GetComponent<Collider2D>();
        for(int i= 0; i < colliders.Length; i++)
        {
            GameObject obj = colliders[i].gameObject;
                
           
            if (playerCollider != colliders[i] && obj.tag == "InnerCircle")
            {
                float distance = Vector2.Distance(this.transform.position, obj.transform.position);
                if(distance < minDistance)
                {
                    closeObject = obj;
                    minDistance = distance;
                }
            }
        }
        Debug.Log("Closest Obj Name : " + closeObject.name );
        return closeObject;
    }



}
