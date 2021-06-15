using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    public int attackDamage = 1;

    void Update()
    {
      if(Input.GetKeyDown(KeyCode.Space))
      {
        attackNow();
      }
    }


    public void attackNow()
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
}
