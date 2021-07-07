using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiMovementScript : MonoBehaviour
{
  public Animator animator;
  public float moveSpeed = 5f;
  public float rotSpeed = 5f;
  public Transform movePoint;
  Transform rotPoint;
  public EnemiAttack attackScript;
  public LayerMask whatStopsMovement;
  public string moveAnimationName;

  public int curentOrientation = 1;
  int moveDirX = 0;
  int moveDirY = 0;


  void Start()
  {
      movePoint.parent = null;
      setRotation();
  }

  void Update()
  {



      transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);




      Vector2 direction = new Vector3(moveDirX,moveDirY,0f);
      float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
      Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
      transform.rotation = Quaternion.Slerp(transform.rotation, rotation,rotSpeed * Time.deltaTime);



  }


  
  public void animateDeath()
  {
    animator.SetTrigger("Death");
  }
  public void move()
  {
    if(Vector3.Distance(transform.position, movePoint.position) <= .05f)
    {
      if(!Physics2D.OverlapCircle(movePoint.position + new Vector3(moveDirX, moveDirY,0f), .2f, whatStopsMovement))
      {
        movePoint.position += new Vector3(moveDirX,moveDirY,0f);
        animator.Play(moveAnimationName);


      }
    }
  }



public void attack()
{

  attackScript.enemyAttack();

}

public void enemiBoom()
{
  attackScript.enemyGoesBoom();
}


  public void InputLeft()
  {
    switch (curentOrientation)
    {
      case 1:
        curentOrientation = 4;
        break;
      case 2:
        curentOrientation = 3;
        break;
      case 3:
        curentOrientation = 1;
        break;
      case 4:
        curentOrientation = 2;
        break;
    }
    setRotation();
  }

  public void InputRight()
  {
    switch (curentOrientation)
    {
      case 1:
        curentOrientation = 3;
        break;
      case 2:
        curentOrientation = 4;
        break;
      case 3:
        curentOrientation = 2;
        break;
      case 4:
        curentOrientation = 1;
        break;
    }
    setRotation();
  }

  void setRotation()
  {
    switch (curentOrientation)
    {
      case 1:



        moveDirX = -1;
        moveDirY = 0;
        //transform.rotation = Quaternion.Euler(0, 0, -180);
        //Left
        break;
      case 2:
        moveDirX = 1;
        moveDirY = 0;
      //  transform.rotation = Quaternion.Euler(0, 0, 0);
        break;
        //Right
      case 3:
        moveDirX = 0;
        moveDirY = 1;
    //    transform.rotation = Quaternion.Euler(0, 0, 90);
        break;
        //Up
      case 4:
        moveDirX = 0;
        moveDirY = -1;
    //    transform.rotation = Quaternion.Euler(0, 0, -90);
        break;
        //Down
    }
  }
}
