using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovementScript : MonoBehaviour
{
  private int currentTower = 0;
  public Animator animator;
  public float moveSpeed = 5f;
  public float rotSpeed = 5f;
  public Transform movePoint;
  Transform rotPoint;
  public Attack attackScript;
  public LayerMask whatStopsMovement;
  public string moveAnimationName;

  public int curentOrientation = 1;
  int moveDirX = 0;
  int moveDirY = 0;

  public Sprite[] baseSprites;
  public GameObject baseComponent;
  public Sprite[] towerSprites;
  public GameObject towerSprite;
  private int currentBaseSprite = 0;
  void Awake()
  {
    currentBaseSprite = PlayerPrefs.GetInt("currentBaseSprite");
    currentTower = PlayerPrefs.GetInt("currentTower");
  }
  void Start()
  {

      SetSprite(currentBaseSprite,currentTower);
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


  public void death()
  {
    PlayerPrefs.SetInt("isExecutingNeeded", 0);
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }
  public void animateDeath()
  {
    animator.Play("Player_death");
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


public void pAttack()
{
  switch (currentTower)
  {
    case 0:
      attackScript.playerAttack();
      break;
    case 1:
      attackScript.playerAueAttack();
      break;
    case 2:
      attackScript.playerLongAttack();
      break;
  }
}
public void attack()
{
  switch (currentTower)
  {
    case 0:
      animator.Play("Player_attack");
      break;
    case 1:
      animator.Play("WideAttack");
      break;
    case 2:
      animator.Play("LongAttack");
      break;
  }


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

  void SetSprite(int index,int number)
  {
    baseComponent.GetComponent<SpriteRenderer> ().sprite = baseSprites[index];
    towerSprite.GetComponent<SpriteRenderer> ().sprite = towerSprites[number];
  }





}
