using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public LayerMask playerLayer;
    public GameObject baseScript;
    int attackDamage = 100;




    public void playerAttack()
    {
      Collider2D[] hitEnemys = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

      foreach(Collider2D enemy in hitEnemys)
      {
        enemy.GetComponent<EnemyBase>().TakeDamage(attackDamage);

      }
    }
    void OnDrawGizmosSelected()
    {
      if(attackPoint == null)
        return;

      Gizmos.DrawWireSphere(attackPoint.position,attackRange);
    }
    public void enemyAttack()
    {
      Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

      foreach(Collider2D player in hitPlayer)
      {
        player.GetComponent<MovementScript>().animateDeath();
      }
    }
    public void enemyGoesBoom()
    {
      Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

      foreach(Collider2D player in hitPlayer)
      {
        baseScript.GetComponent<EnemyBase>().TakeDamage(attackDamage);
      }
    }
}
