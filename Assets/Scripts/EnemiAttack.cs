using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiAttack : MonoBehaviour
{
  public Transform attackPoint;
  public float attackRange = 0.5f;
  public LayerMask playerLayer;
  public GameObject baseScript;
  public Animator anim;
  private Collider2D[] hitPlayer;



  void OnDrawGizmosSelected()
  {
    if(attackPoint == null)
      return;

    Gizmos.DrawWireSphere(attackPoint.position,attackRange);
  }
  public void enemyAttack()
  {
    hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

    foreach(Collider2D player in hitPlayer)
    {
      anim.SetTrigger("attack");
    }
  }

  public void enemyGoesBoom()
  {
    Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

    foreach(Collider2D player in hitPlayer)
    {
      baseScript.GetComponent<EnemyBase>().TakeDamage();
    }
  }
  public void playerTakeDamage()
  {
    foreach(Collider2D player in hitPlayer)
    {
      player.GetComponent<MovementScript>().animateDeath();
    }
  }
  public void StataicAttack()
  {
    Collider2D[] htPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

    foreach(Collider2D player in htPlayer)
    {
      player.GetComponent<MovementScript>().animateDeath();
    }
  }
}
