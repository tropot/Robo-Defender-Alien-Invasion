using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{

    public EnemiMovementScript ms;
    int i = 0;
    public int maxHealth = 2;
    int currentHealth;
    public List<int> order = new List<int>();
    public bool isBig = false;
    float timeRemaining = 0.1f;
    GameController gc;
    private bool Died = false;

    void Start()
    {
      currentHealth = maxHealth;
      gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }


    void Update()
    {
      if (timeRemaining > 0)
      {
        timeRemaining -= Time.deltaTime;
      }
      else
      {
        if(isBig == true)
        {
          ms.attack();
        }
        else
        {
          ms.enemiBoom();
        }

        timeRemaining = 0.1f;

        //started = false;
      }
    }

    public void TakeDamage()
    {

        if(Died == false)
        {
          Died = true;
          ms.animateDeath();
        }

    }

    public void Die()
    {
      gc.nrOfEnemies -= 1;
      Destroy(gameObject);
    }

    public void matchAction()
    {
      if(order.Count > i)
      {

        switch (order[i])
        {
          case 1:
            //move
            ms.move();
            break;
          case 2:
            //right
            ms.InputRight();
            break;
          case 3:
            //left
            ms.InputLeft();
            break;
          case 4:
            //attack
            //ms.attack();
            break;
        }
        i += 1;
      }
    }


}
