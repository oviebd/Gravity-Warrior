using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    public float movingSpeed = 5.0f;
    public float rotateSpeed = 200.0f;
    public Transform target;

    public float rotateSpeedAround = 200.0f;
    public float movingSpeedAround = 5.0f;

    public enum movingState { MoveUp, TowardAPosition, RotateAround };

    public movingState currentMovingState = movingState.MoveUp;


  
   public float deltaAlpha = 0.01f;
   public float a = 1f;

   public float radious = 1.0f;
   float angularSpeed = 2.0f;
   float posX, posY = 0.0f;
   float alpha = 0.0f;


    bool isRotationStart = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        currentMovingState = movingState.MoveUp;
    }

   
    void Update()
    {
        //Debug.Log("Current State : " + currentMovingState);
       switch (currentMovingState)
        {
            case movingState.MoveUp:
                MoveUp();
                break;
           case movingState.TowardAPosition:
                MoveTowardsAposition();
                break;
           case movingState.RotateAround:
               MoveRotateARound();
                break;
        }
    }

    public void MoveUp()
    {
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

    public void MoveRotateARound()
    {

       /* Vector2 center = target.position;
        rb.velocity = Vector2.zero;
        transform.position = new Vector2(center.x + a*Mathf.Cos(alpha), center.y + a*Mathf.Sin(alpha)) ;
        alpha = alpha - deltaAlpha;*/

        if(isRotationStart == false)
        {
            isRotationStart = true;
            alpha = Utils.GetAngle(target.position, this.gameObject.transform);
           // alpha = alpha * Mathf.Deg2Rad;
  
            Debug.Log("init Angle is : " + alpha);
        }

        Transform center = target;

        posX = center.position.x + Mathf.Cos(alpha) * radious;
        posY = center.position.y + Mathf.Sin(alpha) * radious;

        transform.position = new Vector2(posX, posY);
        alpha = alpha - Time.deltaTime * angularSpeed;
        Debug.Log(" Angle is : " + alpha);

        if (alpha >= 360)
            alpha = 0;

        Vector3 dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis( angle, Vector3.forward);

    }

    
}
