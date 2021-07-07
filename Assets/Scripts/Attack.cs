using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    public Transform attackPoint;
    public Transform attackPointOne;
    public Transform attackPointTwo;
    public Transform attackPointThree;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;




    public void playerAttack()
    {
      Collider2D[] hitEnemys = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

      foreach(Collider2D enemy in hitEnemys)
      {
        enemy.GetComponent<EnemyBase>().TakeDamage();
      }
    }
    void OnDrawGizmosSelected()
    {
      if(attackPoint == null)
        return;

      Gizmos.DrawWireSphere(attackPoint.position,attackRange);
    }
    public void playerLongAttack()
    {
      Collider2D[] hitEnemys = Physics2D.OverlapAreaAll(attackPoint.position,attackPointOne.position, enemyLayers);
      foreach(Collider2D enemy in hitEnemys)
      {
        enemy.GetComponent<EnemyBase>().TakeDamage();
      }
    }
    public void playerAueAttack()
    {
      Collider2D[] hitEnemys = Physics2D.OverlapAreaAll(attackPointTwo.position,attackPointThree.position, enemyLayers);
      foreach(Collider2D enemy in hitEnemys)
      {
        enemy.GetComponent<EnemyBase>().TakeDamage();
      }
    }

}
