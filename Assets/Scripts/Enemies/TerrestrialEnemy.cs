using UnityEngine;

public class TerrestrialEnemy : EnemyAI
{
    private void Awake()
    {
        //initialization
        anim = GetComponent<Animator>();
        enemyRenderer = GetComponent<SpriteRenderer>();
        MovementSpeed = 1.5f;
        CurrentHealth = MAXHealth;
        WeaponDamage = 20;
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        path_movementDistance = new Vector3(3f, 0, 0);
        left_path_position = transform.position - path_movementDistance;
        right_path_position = transform.position + path_movementDistance;
        Rb2D = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {

        if (Vector2.Distance(transform.position, Target.position) <= AttackRange)
        {
          
            Attack();
            CanAttack = false;
                
        }


        if (Vector2.Distance(transform.position, Target.position) > followingDistance)
        {
            MovementPath();
        }
        else
            FollowPlayer();


        if (Rb2D.velocity != Vector2.zero)
            anim.SetBool("IsWalking",true);
        
        else
            anim.SetBool("IsWalking", false);


        if (isDead)
            this.enabled = false;
        

    }


    public override void MovementPath()
    {
       
        if (movingLeft)
        {
           
            enemyRenderer.flipX = true;

            if (Vector2.Distance(transform.position, left_path_position) == 0)
            {
                movingLeft = false;
            }
               

            else 
                transform.position = Vector3.MoveTowards(transform.position, left_path_position, MovementSpeed * Time.deltaTime);
        }

        else
        {
            
            enemyRenderer.flipX = false;

            if (Vector2.Distance(transform.position, right_path_position) == 0)
            {
                movingLeft = true;
            }
               
           
            else
                transform.position = Vector3.MoveTowards(transform.position, right_path_position, MovementSpeed * Time.deltaTime);
        }
    }


    
}
