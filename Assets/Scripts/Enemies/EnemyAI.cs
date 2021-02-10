using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public abstract class EnemyAI : EnemyBase
{
    
    protected float followingDistance = 5f;
    protected Vector3 left_path_position;
    protected Vector3 right_path_position;
    protected Vector3 path_movementDistance; 
    protected bool movingLeft = true;

    public void Attack()
    {
        if (CanAttack)
        {
            anim.SetTrigger("EnemyAttack");
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, AttackRange, enemyLayer);

            foreach (Collider2D player in hitEnemies)
            {
                //si el jugador ha muerto el enemigo no seguirá atacando
                if (!player.GetComponent<Player>().died)
                {
                    player.GetComponent<Player>().TakeDamage(WeaponDamage);

                }

            }
        }
    }

    public abstract void MovementPath();
   

    protected void FollowPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector3(Target.position.x,transform.position.y,transform.position.z), (MovementSpeed * 1.5f) * Time.deltaTime);
        if (Target.position.x > transform.position.x)
        {
            enemyRenderer.flipX = false;
        }
        else if (Target.position.x < transform.position.x)
        {
            enemyRenderer.flipX = true;
        }
    }

}
