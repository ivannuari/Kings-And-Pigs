using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class player : MonoBehaviour
{
    //parameter
    public float speed = 5f;
    public float jumpforce = 5f;
    public int maxHealth = 3;
    public int currHealth;
    public int attackDamage = 2;
    //parameter for groundcheck
    private float moveX;
    private bool isGrounded = true;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask WhatisGround;

    public Transform attackPoint;
    public float attackRadius;
    public LayerMask EnemyLayer;

    public float requestTime = 1f;
    float cooldown = 0;

    public GameObject[] livecounts;

    //sound effect
    public AudioClip step;
    public AudioClip getHit;
    public AudioClip getJump;
    public AudioClip getAttack;

    //component
    Rigidbody2D rb;
    SpriteRenderer render;
    Animator anim;
    AudioSource sound;
    public FloatingJoystick variableJoystick;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        sound = GetComponent<AudioSource>();

        currHealth = maxHealth;
        cooldown = requestTime;
    }

    void Update()
    {
        //Input for joystick
        moveX = variableJoystick.Horizontal;

        //flip renderer
        if(moveX == -1)
        {
            render.flipX = true;
        }
        if(moveX == 1)
        {
            render.flipX = false;
        }

        //cooldown for attack 
        cooldown -= Time.deltaTime;
        
        //motion for walk
        anim.SetFloat("jalan", rb.velocity.magnitude);

        if(Input.GetButtonDown("Jump"))
        {
           Jump();
        }
        anim.SetBool("lompat", isGrounded);
    }

    public void Jump()
    {
        if(isGrounded)
        {
            sound.PlayOneShot(getJump , 1f);
             rb.velocity = transform.up * jumpforce;
        }
    }

    public void footstep()
    {
        sound.PlayOneShot(step , 1f);
    }

    void FixedUpdate()
    {
        //movement rigidbody2d
        rb.velocity = new Vector2(moveX * speed , rb.velocity.y);
        
        //check the ground with boolean
        isGrounded = Physics2D.OverlapCircle(groundcheck.position,checkRadius,WhatisGround);
    }

    public void Attack()
    {
        if(cooldown <= 0)
        {
            anim.SetTrigger("attack");
            sound.PlayOneShot(getAttack , 1f);

            Collider2D[] enemyCollider = Physics2D.OverlapCircleAll(attackPoint.position , attackRadius , EnemyLayer);

            foreach (Collider2D enemy in enemyCollider)
            {
                enemy.GetComponent<enemy>().TakeDamage(attackDamage);
            }
            cooldown = requestTime; 
        }
    }

    void LiveCounting(int i)
    {
        livecounts[currHealth].SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        anim.SetTrigger("hit");
        sound.PlayOneShot(getHit , .7f);
        currHealth -= damage;
        LiveCounting(-1);
        if(currHealth <= 0)
            Die();
    }

    void Die()
    {
        anim.SetBool("dead",true);
        gameObject.SetActive(false);
        SceneManager.LoadScene("Menu");
        Debug.Log("player die !!");
    }

    void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        return;

        if(groundcheck == null)
        return;

        Gizmos.DrawWireSphere(groundcheck.position , checkRadius);
        Gizmos.DrawWireSphere(attackPoint.position , attackRadius);
    }
}
