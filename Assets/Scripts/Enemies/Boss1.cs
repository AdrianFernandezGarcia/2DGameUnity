using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Rendering;

public class Boss1 : Boss
{
    private float confusedTime;
    private float attackCooldown;
    private static Boss1 instance;
    private CircleCollider2D circleColider;
    private bool stop=false;
    [SerializeField]
    private EnemyHealthBar healthBar;
    private void Awake()
    {
        MAXHealth = 100;
        CurrentHealth = MAXHealth;
        healthBar.SetMaxHealth(MAXHealth);
        path_movementDistance = new Vector3(14f, 0, 0);
        MovementSpeed = 8f;
        left_path_position -= path_movementDistance;
        right_path_position += path_movementDistance;
        Rb2D = GetComponent<Rigidbody2D>();
        circleColider = GetComponent<CircleCollider2D>();
        enemyRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        confusedTime = 2f;
        attackCooldown = 3f;
        
      

    }

    void Update()
    {
        

        Target = GameObject.FindGameObjectWithTag("Player").transform;

        healthBar.SetHealth(CurrentHealth);

        if (CurrentHealth > 300)
        {
            Phase1();

        }

        else if (CurrentHealth < 300 && CurrentHealth >100 )
        {
            confusedTime = 1.5f;
            Phase2();
            
        }

        else if (CurrentHealth <= 100)
        {
            confusedTime = 1f;
            attackCooldown = 1f;
            Phase3();
            
        }





        if (isDead)
        {
            BossDie();
            enabled = false;
        }



    }

    public override void Phase1()
    {
      
            MovementPath();
            
            if(Vector2.Distance(transform.position,Target.position) <= AttackRange)
            Attack();
            
    }

    public override void Phase2()
    {

        if (CanAttack)
        {
            Attack();
            CanAttack = false;
        }
        else
            MovementPath();

    }

    public override void Phase3()
    {
        if (CanAttack)
        {
            Attack();
            CanAttack = false;
            StartCoroutine(HabilityCooldown());
        }
        else
        MovementPath();
           

           
     }

    public override IEnumerator EnemyConfused()
    {
        yield return new WaitForSeconds(confusedTime);
        this.enabled = true;
        anim.enabled = true;


    }

    public override IEnumerator HabilityCooldown()
    {
       
        yield return new WaitForSeconds(attackCooldown);
        CanAttack = true;


    }

    public void BossDie()
    {
        healthBar.gameObject.SetActive(false);
        circleColider.offset = new Vector2(-0.18f, 1.72f);
    }
    
    //GETTERS Y SETTERS

    public bool getIsDead()
    {
        return isDead;
    }

    public void setIsDead(bool isDead)
    {
        this.isDead = isDead;
    }
}
