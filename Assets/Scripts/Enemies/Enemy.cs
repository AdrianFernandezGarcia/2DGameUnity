using System.Collections;
using UnityEngine;

public class Enemy : EnemyBase
{
    [SerializeField]
    private EnemyHealthBar bar;
    private EnemyAI movement;
    private void Awake()
    {
        MAXHealth = 100;
        WeaponDamage = 40;
        AttackRange = 1.5f;
        movement = GetComponent<EnemyAI>();
    }

    private void Start()
    {
        CurrentHealth = MAXHealth;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, AttackRange);
    }



    public void TakeDamage(int dmg)
    {
        anim.SetTrigger("EnemyHurt");

        CurrentHealth -= dmg;

        bar.SetHealth(CurrentHealth);

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    private IEnumerator Disable()
    {

        yield return new WaitForSeconds(.2f);
        isDead = true;
        GetComponent<BoxCollider2D>().offset = new Vector2(-0.186086f, -1.72739f);
        GetComponent<BoxCollider2D>().size = new Vector2(7.186255f, 0.241974f);
        anim.enabled = false;
        enemyRenderer.sprite = deathSprite;
        bar.gameObject.SetActive(false) ;
        this.enabled = false;
        movement.enabled = false;

    }


    public void Die()
    {
        anim.SetBool("isDead", true);
        StartCoroutine(Disable());
    }



    #region GETTERS & SETTERS

    public int GetMaxHealth()
    {
        return MAXHealth;
    }

    public Sprite GetDeathSprite()
    {
        return deathSprite;
    }

    #endregion

}

