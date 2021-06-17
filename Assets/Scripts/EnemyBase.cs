using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{

    public MovementScript ms;
    int i = 0;
    public int maxHealth = 2;
    int currentHealth;
    public List<int> order = new List<int>();



    void Start()
    {
      currentHealth = maxHealth;
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0 )
        {
          Die();
        }
    }

    void Die()
    {
      Debug.Log("enemy died");
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
