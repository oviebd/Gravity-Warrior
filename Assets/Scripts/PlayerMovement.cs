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

    private IMove _rotateAround;
    private IMove _moveTowardsTarget;
    private IMove _moveTowardsDirection;
  
  
    private PlanetData _planetData;

   
    bool isdirectedForceStart = false;

    bool isCloseObjectGet = false;
    GameObject closeObject = null;
    public bool isPlayerDocked = false;

    private MovingData _moveData = new MovingData();

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        currentMovingState = movingState.MoveUp;
        _rotateAround = GetComponent<RotateAround>();
        _moveTowardsTarget = GetComponent<MoveTowardsTarget>();
        _moveTowardsDirection = GetComponent<MoveTowardsDirection>();

        SetMoveState(movingState.MoveUp);
    }

    public void SetData(PlanetData planetData, GameObject targetObj)
    {
        target = targetObj.transform;
        _planetData = planetData;
       
        isdirectedForceStart = false;

        _moveData = new MovingData();
        _moveData.targetObj = targetObj;
        _moveData.rotationSpeed = _planetData.playerRotationSpeed;
        _moveData.movingSpeed = _planetData.playerMovingSpeed;
        _moveData.torque = _planetData.playerTorque;
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
       
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isPlayerDocked == true)
                SetMoveState(movingState.ForceTowardsADirection);
          
            else
                SetMoveState(movingState.FreeJump);
        }
    }

    public void SetMoveState(movingState state)
    {
        currentMovingState = state;

        _moveTowardsTarget.StopMove();
        _rotateAround.StopMove();
        _moveTowardsDirection.StopMove();

        switch (state)
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
    }

    public void PlayerNormalMove()
    {
        PlayerNormalMove(movingSpeed);
    }
    public void PlayerNormalMove(float speed)
    {
        isPlayerDocked = false;
        unparentRocket();
        _moveData.direction = Vector3.up;
        _moveData.movingSpeed = speed;

        _moveTowardsDirection.SetUp(_moveData);
        _moveTowardsDirection.StartMove();
    }

    public void MoveTowardsAposition()
    {
        if (_planetData == null)
            return;
       // isPlayerDocked = true;
     
       MoveTowardsAposition(target, _planetData.playerTorque, _planetData.playerMovingSpeed);
       
    }

    public void MoveTowardsAposition(Transform target,float rotateSpeed,float movingSpeed)
    {
        _moveData.targetObj = target.gameObject;
        _moveData.torque = rotateSpeed;
        _moveData.movingSpeed = movingSpeed;

        _moveTowardsTarget.SetUp(_moveData);
        _moveTowardsTarget.StartMove();

    }

   

    public void MoveRotateARound()
    {
        if (target == null || _planetData == null)
            return;

        isPlayerDocked = true;

        _rotateAround.SetUp(_moveData);
        _rotateAround.StartMove();
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
            closeObject = GetGameobjectsWithinARadious(50.0f);
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
        return closeObject;
    }



}
