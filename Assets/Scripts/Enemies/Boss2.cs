using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : Boss
{
    
    private float confusedTime;
    private float jumpCooldown;
    [SerializeField]
    private EnemyHealthBar healthBar;
    private bool canJump = true;
    
    
    // Start is called before the first frame update
    void Start()
    {
        if (isDead)
        {
            healthBar.gameObject.SetActive(false);
            enabled = false;
            anim.enabled = false;
            enemyRenderer.sprite = deathSprite;
        }
        MAXHealth = 100;
        CurrentHealth = MAXHealth;
        healthBar.SetMaxHealth(MAXHealth);
        path_movementDistance = new Vector3(14f, 0, 0);
        MovementSpeed = 5f;
        left_path_position -=path_movementDistance;
        right_path_position += path_movementDistance;
        Rb2D = GetComponent<Rigidbody2D>();
        //circleColider = GetComponent<CircleCollider2D>();
        enemyRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        jumpCooldown = 3f;
        confusedTime = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        Target = GameObject.FindGameObjectWithTag("Player").transform;

        healthBar.SetHealth(CurrentHealth);

        //cuando su vida sea mayor de 300
        if (CurrentHealth > 300)
        {
            Phase1();

        }

        //cuando su vida esté entre 300 y 100
        else if (CurrentHealth <= 300 && CurrentHealth > 100)
        {
            MovementSpeed = 8;
            confusedTime = 1.5f;
            Phase2();

        }

        else if (CurrentHealth <= 100)
        {
            MovementSpeed = 10;
            confusedTime = 1f;
            Phase3();

        }

        //desactivar el script si el jefe ha muerto
        if (isDead)
        {
            enabled = false;
        }
    }

    public override IEnumerator EnemyConfused()
    {
        yield return new WaitForSeconds(confusedTime);
        this.enabled = true;
        anim.enabled = true;


    }

    public override IEnumerator HabilityCooldown()
    {

        yield return new WaitForSeconds(jumpCooldown);
        canJump = true;


    }

    public override void Phase1()
    {

        MovementPath();
    }

    public override void Phase2()
    {
        if (canJump && (Vector2.Distance(transform.position, left_path_position) <3 || Vector2.Distance(transform.position, right_path_position) <3))
        {
            //saltar
            Rb2D.velocity = new Vector3(Rb2D.velocity.x, 10, 0f);
            canJump = false;
            StartCoroutine(HabilityCooldown());
        }

         MovementPath();
    }

    public override void Phase3()
    {
        if (canJump && (Vector2.Distance(transform.position, left_path_position) < 3 || Vector2.Distance(transform.position, right_path_position) < 3))
        {
            Rb2D.velocity = new Vector3(Rb2D.velocity.x, 10, 0f);
            canJump = false;
            StartCoroutine(HabilityCooldown());
        }

        MovementPath();
    }

    
}
