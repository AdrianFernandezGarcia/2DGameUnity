using System.Collections;
using UnityEngine;

public class PlayerAttack : Player_Base
{
	private float attackRange = 2f;
	private int weaponDamage = 40;
	[SerializeField]
	private Transform attackPoint;
	[SerializeField]
	protected LayerMask enemyLayer;
	private bool canAttack = true;
	
	void Update()
    {
		if (Input.GetButtonDown("Fire1") && canAttack)
		{
			Attack();

		}
	}

	private void Attack()
	{

		anim.SetTrigger("PlayerAttack");
		//SoundManager.Instance.PlayClip(swordSound);
		Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

		foreach (Collider2D enemy in hitEnemies)
		{
			if (enemy.GetComponent<Enemy>() != null)
            	enemy.GetComponent<Enemy>().TakeDamage(weaponDamage);
			

			else if (enemy.GetComponent<Lever>() != null)
				enemy.GetComponent<Lever>().ActivateLever();
			

			canAttack = false;
			StartCoroutine(AttackCooldown());
		}

		canAttack = false;
		StartCoroutine(AttackCooldown());


	}

	IEnumerator AttackCooldown()
	{
		yield return new WaitForSeconds(.3f);
		canAttack = true;
	}

	//DEBUG ONLY
	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawWireSphere(attackPoint.position, attackRange);
	}
}
