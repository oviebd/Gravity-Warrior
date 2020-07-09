using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    [SerializeField] private float _movingSpeed = 5.0f;
    [SerializeField] private bool _isPlayerDocked = false;

    public enum movingState { TowardAPosition, RotateAround ,MoveTowardsAdirection,DockedInLauncher,FreeJump};
    private movingState currentMovingState;

    private IMove _rotateAround;
    private IMove _moveTowardsTarget;
    private IMove _moveTowardsDirection;
  
    private IMoveData _moveData;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        _moveData = new MoveData();
    }

    void Start()
    {
        _rotateAround = GetComponent<RotateAround>();
        _moveTowardsTarget = GetComponent<MoveTowardsTarget>();
        _moveTowardsDirection = GetComponent<MoveTowardsDirection>();

        SetMoveState(movingState.MoveTowardsAdirection);
    }

    public void SetData(IMoveData moveData)
    {
        _moveData = moveData.DeepCopy(moveData);
    }
   
    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_isPlayerDocked == true)
                SetMoveState(movingState.MoveTowardsAdirection);
          
            else
                SetMoveState(movingState.FreeJump);
        }

        //Debug.Log(_moveData.movingSpeed + " State : " + currentMovingState  + "  Speed :  " + _moveData.movingSpeed);
    }

    public void SetMoveState(movingState state)
    {
        currentMovingState = state;
        Utils.StopOrStartMovementOfAobj(this.gameObject, false);

        switch (state)
        {
            case movingState.TowardAPosition:
                MoveTowardsAposition();
                break;
            case movingState.RotateAround:
                MoveRotateARound();
                break;
            case movingState.MoveTowardsAdirection:
                MoveTowardsAdirection();
                break;
            case movingState.DockedInLauncher:
                DockInLauncher();
                break;
       
            case movingState.FreeJump:
                GoToClosestPlanet();
                break;
        }
    }


    void MoveTowardsAdirection()
    {
        _isPlayerDocked = false;
        transform.parent = null;

        _moveData.direction = Vector3.up;
        if (_moveData.movingSpeed <= 0)
            _moveData.movingSpeed = _movingSpeed;

        _moveTowardsDirection.SetUp(_moveData);
        _moveTowardsDirection.StartMove();
    }

    public void MoveTowardsAposition()
    {
        _moveTowardsTarget.SetUp(_moveData);
        _moveTowardsTarget.StartMove();
    }

    public void MoveRotateARound()
    {
        _isPlayerDocked = true;

        _rotateAround.SetUp(_moveData);
        _rotateAround.StartMove();
    }

    public void DockInLauncher()
    {
        transform.rotation = new Quaternion(0, 0, 0, 0);
        transform.parent = _moveData.targetObj.transform;
        transform.localPosition = Vector2.zero;
    }
   
    void GoToClosestPlanet()
    {
        GameObject closeObject = Utils.GetGameobjectsWithinARadious(50.0f, GameEnum.Tags.InnerCircle.ToString(), this.gameObject);

        if (closeObject == null)
            return;

        _moveData.targetObj = closeObject;
        _moveData.torque = 1000.0f;
        _moveData.movingSpeed = 30.0f;

        MoveTowardsAposition();
    }

}
