using System.Collections;
using UnityEngine;

public abstract class Boss : EnemyAI
{

    protected bool jumpReady = true;
    protected bool grounded = true;
    protected bool sided = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
        enemyRenderer = GetComponent<SpriteRenderer>();
        path_movementDistance = new Vector3(15f, 0, 0);
        left_path_position = transform.position - path_movementDistance;
        right_path_position = transform.position + path_movementDistance;
    }

    public override void MovementPath()
    {  
        if (movingLeft)
        {

            if (Vector2.Distance(transform.position, left_path_position) == 0)
            {
                anim.SetBool("IsRunning", false);
                this.enabled = false;
                sided = true;
                
                StartCoroutine(EnemyConfused());
                movingLeft = false;
                
            }
            else
            {
                sided = false;
                enemyRenderer.flipX = true;
                transform.position = Vector2.MoveTowards(transform.position, left_path_position, MovementSpeed * Time.deltaTime);
                anim.SetBool("IsRunning", true);
            }
        }

        else if (!movingLeft)
        {

            if (Vector2.Distance(transform.position, right_path_position) == 0)
            {
                anim.SetBool("IsRunning", false);
                this.enabled = false;
                sided = true;
                StartCoroutine(EnemyConfused());
                movingLeft = true;
            }
            else
            {
                sided = false;
                transform.position = Vector2.MoveTowards(transform.position, right_path_position, MovementSpeed * Time.deltaTime);
                anim.SetBool("IsRunning", true);
                enemyRenderer.flipX = false;
            }
        }
    }

    public abstract IEnumerator EnemyConfused();

    public abstract IEnumerator HabilityCooldown();
   

    public abstract void Phase1();


    public abstract void Phase2();


    public abstract void Phase3();
   

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }

        if (collision.gameObject.CompareTag("Player") && !isDead)
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(WeaponDamage);
        }
    }

    public void ResetSprite()
    {
        enemyRenderer.sprite = deathSprite;
    }

    public void DisableAnim()
    {
        anim.enabled = false;
    }

    public void EnableAnim()
    {
        anim.enabled = true;
    }

    //GETTERS Y SETTERS


    public bool GetIsDead()
    {
        return isDead;
    }

    public void SetIsDead(bool isDead)
    {
        this.isDead = isDead;
    }

    public int GetMaxHealth()
    {
        return MAXHealth;
    }

    public void SetCurrentHealth(int health) => CurrentHealth = health;
    
}
