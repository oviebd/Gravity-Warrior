using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    public float movingSpeed = 5.0f;
   

    public enum movingState { MoveUp, TowardAPosition, RotateAround ,ForceTowardsADirection,StopWithUpwordDirection,pocketShoot,FreeJump};

    public movingState currentMovingState = movingState.MoveUp;

    private IMove _rotateAround;
    private IMove _moveTowardsTarget;
    private IMove _moveTowardsDirection;
  
  
    private IMoveData _moveData;

   
    bool isdirectedForceStart = false;

    bool isCloseObjectGet = false;
    GameObject closeObject = null;
    public bool isPlayerDocked = false;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _moveData = new MoveData();
    }

    void Start()
    {
        _rotateAround = GetComponent<RotateAround>();
        _moveTowardsTarget = GetComponent<MoveTowardsTarget>();
        _moveTowardsDirection = GetComponent<MoveTowardsDirection>();

        SetMoveState(movingState.MoveUp);
    }

    public void SetData(IMoveData moveData)
    {
       
        _moveData = moveData.DeepCopy(moveData);
     //   Debug.Log("Set Move data with speed : " + moveData.movingSpeed);
        isdirectedForceStart = false;
    }

    public void ResetData()
    {
        //_moveData = null;
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

       // Debug.Log(_moveData.movingSpeed + " State : " + currentMovingState );
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
               // _moveData.movingSpeed = movingSpeed;
                PlayerNormalMove();
                break;
            case movingState.TowardAPosition:
                MoveTowardsAposition();
                break;
            case movingState.RotateAround:
                MoveRotateARound();
                break;
            case movingState.ForceTowardsADirection:
               // _moveData.movingSpeed = movingSpeed;
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
        isPlayerDocked = false;
        unparentRocket();

        _moveData.direction = Vector3.up;
        if (currentMovingState == movingState.MoveUp)
            _moveData.movingSpeed = movingSpeed;

        _moveTowardsDirection.SetUp(_moveData);
        _moveTowardsDirection.StartMove();
    }

    public void MoveTowardsAposition()
    {
       
        // isPlayerDocked = true;

        _moveTowardsTarget.SetUp(_moveData);
        _moveTowardsTarget.StartMove();
       
    }

    public void MoveRotateARound()
    {
       if (_moveData.targetObj == null)
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
        _moveData.movingSpeed = movingSpeed;
        PlayerNormalMove();
    }


    public void StopWithUpwordDirection()
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0.0f;
        transform.rotation = new Quaternion(0, 0, 0, 0);
        transform.parent = _moveData.targetObj.transform;
        transform.localPosition = Vector2.zero;

        _moveData.movingSpeed = 0.0f;
    }

    public void ShootFromPocket()
    {
        _moveData.movingSpeed = 50.0f;
        PlayerNormalMove();

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

        _moveData.targetObj = closeObject;
        _moveData.torque = 1000.0f;
        _moveData.movingSpeed = 30.0f;

        MoveTowardsAposition();

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
